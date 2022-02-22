# E-Flow Doküman Direkt Link Web Servisi
E-Flow süreçlerindeki doküman bileşenleri için direkt link sunan web servistir.

Proje self-host web api mimarisinde olup, topshelf projesi sayesinde windows servisi olarak çalışacaktır.

Süreç CIID ve Bileşen DID değeri ile sürecin ilgili bileşenindeki dosyayı indirmenize olanak tanır.

E-Flow veri tabanındaki INST_DATA_ATTACHMENT tablosunu kullanarak işlem yapar.


#### ⚠ Genel uyarılar
* Bu proje firma içi ağda basit kullanım amaçlanarak hazırlanmıştır.
* Projede hiçbir güvenlik önlemi yoktur! Veri güvenliğinden kullanıcı sorumludur.
* Geliştirilmesi devam eden bir projedir.

### Servisin ayarlanması
Tüm ayarlar Config dosyası içerisinde yer almaktadır. Uygulama dizinindeki "eflowDirektLinkService.exe.config" dosyası notepad ile açılarak değişiklikler yapılabilir.

Servis kurulumundan önce config dosyası içerisinde doğru bilgilerin yer aldığından emin olun.

| Alan | Açıklama |
|------|----------|
|EFlowConnection|MSSQL Connection string ifadesi. Varsayılan veri tabanı olarak eflow işaret etmelidir.|
|ServisAdresi|Rest servisin yayın yapacağı temel adres. Kullanılan port firewall üzerinden açılmalıdır. |
|GecikmeliBaslat| Servisin gecikmeli başlaması isteniyorsa E değeri verilmelidir.|
|KullaniciTuru|Servisi başlatacak kullanıcıyı belirtir. Tüm değerleri için alttaki tabloya bakın.|
|Kullanici| KullaniciTuru verisi "User" olarak ayarlanmışsa; Windows kullanıcı adı girilmelidir. Domain üyesi bir kullanıcı girilecekse Domain\Kullanıcı formatında olmalıdır.|
|Parola| KullaniciTuru verisi "User" olarak ayarlanmışsa; ilgili kullanıcının parola bilgisi girilmelidir.|

*Servis ayarlarının geçerli olması için servis durdurulmalıdır.*

##### Servis başlatma kullanıcı türleri

|Tür|Açıklama|
|---|--------|
|LocalSystem|LocalSystem kullanıcısıyla başlatır|
|LocalService|LocalSystem kullanıcısıyla başlatır|
|NetworkService|NetworkService kullanıcısıyla başlatır|
|VirtualServiceAccount|VirtualServiceAccount kullanıcısıyla başlatır|
|Prompt|Kullanıcı bilgilerinin girilmesi için pencere görüntüler.|
|User|config dosyası içerisinde girilen kullanıcı bilgileri ile servisi başlatır|

*Varsayılan olarak servis "NetworkService" kullanıcısıyla başlatılacaktır.*


##### Süreç içerisindeki dokünan bileşenlerinin listesinin alınması
Bileşenler DID değeri ile tutuluyor. CIID değeri bilinen bir süreçteki bileşen listesini almak için;
```
{ServisURL}/api/Bilesenler/{CIID}
```
adresine GET isteği atarak cevap alınabilir. Örnek istek ve cevap aşağıdaki gibidir.
```bash
GET http://127.0.0.1:4321/api/Bilesenler/345967
```
```json
[
  {
    "DID": 10404,
    "NAME": "dokuman1",
    "DISPLAYNAME": "Doküman 1"
  },
  {
    "DID": 10405,
    "NAME": "fatura",
    "DISPLAYNAME": "Fatura Görüntüsü"
  }
]
```

##### CIID ve DID verisi ile direkt link ile belgeye ulaşılması
Süreç CIID ve ilgili doküman bileşeninin DID verisi ile istenilen belgeye ulaşmak için;
```
{ServisURL}/api/Dokuman/{CIID}?did={DID}
```
adresine GET isteği atılması yeterlidir. Örnek istek
``` bash
GET http://127.0.0.1:4321/api/Dokuman/6126?did=10404
```

### Yol haritası

|#|Hedef Tarih|Detaylar|
|--|------|---------------|
|✔|Şubat '22 | Selfhost WebAPI servisin oluşturulması|
|✔ | 23 Şubat '22| TopShelf ile windows servis oluşturulması|

### Sorun Giderme

##### Servis başlamıyorsa
* Çalıştır komutu ile services.msc başlatın 
* "Eflow Doküman Servisi" Özellikler penceresini açın
* Oturum Açma sekemesine geçin ve "Aşağıdaki Hesap" seçimini işaretleyin.
* Gözat butonu ile yetkili bir kullanıcı seçin ve parola bilgisinin doğru olduğundan emin olun.

##### Servis çalışıyor ancak cevap vermiyorsa
* Girilen connection string verisinin doğruluğundan emin olun
* Log kayıtlarını inceleyin

#### Kulllanılan Kütüphaneler
* [Microsoft.AspNet.WebApi.SelfHost](https://dotnet.microsoft.com/en-us/apps/aspnet/apis)
* [Dapper Micro ORM](https://github.com/DapperLib/Dapper)
* [Topshelf](https://topshelf-project.com/) <small>*4.3.0 sürümü trojan olarak görülüyor, 4.2.1 sürümü kullanılacak.*</small>
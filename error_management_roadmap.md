# Global Hata ve Mesaj Yönetimi Yol Haritası

Bu doküman, ASP.NET Core API projesinde her endpoint'in standart bir JSON formatında cevap dönmesini ve hataların merkezi bir noktadan yönetilmesini kapsayan aşamaları içerir.

## FAZ 1: Core Result Yapısının Kurulması
API'den dönen tüm cevapları standart bir kalıba sokmak için gerekli `Result` sınıflarının oluşturulması.

- [ ] **DataResult ve Result Sınıfları:** `IsSuccess`, `Message`, `Data` ve `Errors` alanlarını içeren sınıfların yazılması.
- [ ] **BaseResponse:** Tüm API cevaplarının türetileceği temel yapı.

## FAZ 2: Business Layer Entegrasyonu
İş katmanındaki mesaj ve veri yönetiminin bu yeni yapıya geçirilmesi.

- [ ] **IBlogService Güncellemesi:** Metotların `IDataResult<blog>` veya `IResult` dönecek şekilde güncellenmesi.
- [ ] **BlogManager Güncellemesi:** İş kurallarına göre `SuccessResult` veya `ErrorResult` dönülmesi.

## FAZ 3: Global Exception Handling (IExceptionHandler) [TAMAMLANDI]
`try-catch` bloklarını temizleyip, .NET 8+ ile gelen modern `IExceptionHandler` yapısının kurulması.

- [x] **GlobalExceptionHandler:** `IExceptionHandler` arayüzünü uygulayan merkezi hata yakalayıcı sınıfın yazılması.
- [x] **ProblemDetails Entegrasyonu:** Hataların standart `ProblemDetails` formatında (RFC 7807) dönülmesi.
- [x] **Program.cs Kaydı:** `AddExceptionHandler`, `AddProblemDetails` ve `UseExceptionHandler` konfigürasyonlarının yapılması.


## FAZ 4: Validasyon ve Mesaj Yönetimi
Gelen verilerin doğruluğunun kontrol edilmesi ve kullanıcı dostu mesajların yönetilmesi.

- [ ] **FluentValidation:** Modeller için kuralların yazılması.
- [ ] **Otomatik Validasyon Hataları:** Validasyon hatalarının Global Exception Middleware üzerinden Faz 1 formatında dönülmesi.
- [ ] **Sabit Mesajlar:** Mesajların (`BlogAdded`, `BlogNotFound` vb.) bir sınıf içinden yönetilmesi.

---
**Not:** Bu yapı kurulduğunda, Controller katmanınız çok sadeleşecek ve sadece şu şekilde görünecektir:
```csharp
[HttpGet]
public IActionResult GetAll() {
    var result = _blogService.GetAll();
    return result.IsSuccess ? Ok(result) : BadRequest(result);
}
```

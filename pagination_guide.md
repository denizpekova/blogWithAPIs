# ASP.NET Core MVC: Offset Tabanlı Dinamik Sayfalama (Pagination) Rehberi

Modern web uygulamalarında verileri parçalara ayırarak yüklemek (sayfalama), hem kullanıcı deneyimini hem de veritabanı performansını artıran en kritik unsurlardan biridir. Bu rehberde, **SQL Server 2012** ve sonrasında gelen `OFFSET-FETCH` özelliğini kullanarak nasıl profesyonel bir sayfalama sistemi kuracağınızı öğreneceğiz.

---

## 1. Adım: Veri Modelini Oluşturma (PagedResult.cs)

İlk olarak, sayfalama verilerini ve listeyi bir arada tutacak olan generic bir sınıf oluşturuyoruz. Bu sınıf, listemizi sarmalayacak ve sayfa numarası, toplam sayfa sayısı gibi bilgileri tutacaktır.

`Models/PagedResult.cs` dosyasını oluşturun ve aşağıdaki kodları ekleyin:

```csharp
using System;
using System.Collections.Generic;

public class PagedResult<T> : List<T>
{
    public int PageIndex { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }

    public PagedResult(List<T> items, int totalCount, int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        TotalCount = totalCount;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        this.AddRange(items);
    }

    public bool HasPreviousPage => PageIndex > 1;
    public bool HasNextPage => PageIndex < TotalPages;

    public static PagedResult<T> Create(List<T> source, int totalCount, int pageIndex, int pageSize)
    {
        return new PagedResult<T>(source, totalCount, pageIndex, pageSize);
    }
}
```

---

## 2. Adım: SQL OFFSET Mantığını Anlamak

Entity Framework arka planda bunu halletse de, SQL tarafında ne olduğunu bilmek önemlidir. **Offset**, kaç satırın atlanacağını ifade eder.

### SQL Kullanımı:
```sql
DECLARE @pageNumber INT = 1;
DECLARE @pageSize INT = 10;

SELECT * FROM Blogs
ORDER BY BlogID
OFFSET (@pageNumber - 1) * @pageSize ROWS
FETCH NEXT @pageSize ROWS ONLY;
```

---

## 3. Adım: Controller Hazırlığı

Controller tarafında veriyi veritabanından çekerken `Skip` (atla) ve `Take` (al) metodlarını kullanacağız.

```csharp
public IActionResult Index(int? pageNumber)
{
    int pageSize = 10;
    int page = pageNumber ?? 1;
    
    // Kaç kayıt atlanacağını hesaplıyoruz
    int offset = (page - 1) * pageSize;

    try 
    {
        int totalCount = _context.Blogs.Count();
        int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

        // Geçersiz sayfa isteklerini 1. sayfaya yönlendiriyoruz
        if (page < 1 || (totalPages > 0 && page > totalPages))
        {
            return RedirectToAction("Index", new { pageNumber = 1 });
        }

        // Veritabanından sayfalanmış veriyi çekiyoruz
        var blogs = _context.Blogs
            .OrderBy(b => b.Id)
            .Skip(offset)
            .Take(pageSize)
            .ToList();

        // Görünümde buton kontrolü için ViewBag kullanabiliriz
        ViewBag.IsFirstPage = (page == 1);
        ViewBag.IsLastPage = (page == totalPages);

        var result = new PagedResult<Blog>(blogs, totalCount, page, pageSize);
        return View(result);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Hata: " + ex.Message);
        return View("Error");
    }
}
```

---

## 4. Adım: Arayüz (View) Tasarımı

Son olarak, `Index.cshtml` dosyanızda alt kısma "Geri" ve "İleri" butonlarını ekleyerek navigasyonu sağlıyoruz.

```html
@model PagedResult<Blog>

<!-- Blog Listesi Burada Listelenir -->

<div class="pagination-area mt-4">
    @{
        var prevDisabled = !Model.HasPreviousPage ? "disabled" : "";
        var nextDisabled = !Model.HasNextPage ? "disabled" : "";
    }

    @if (!ViewBag.IsFirstPage)
    {
        <a asp-action="Index" 
           asp-route-pageNumber="@(Model.PageIndex - 1)" 
           class="btn btn-dark @prevDisabled">
            Geri
        </a>
    }

    <span class="p-2">Sayfa @Model.PageIndex / @Model.TotalPages</span>

    @if (!ViewBag.IsLastPage)
    {
        <a asp-action="Index" 
           asp-route-pageNumber="@(Model.PageIndex + 1)" 
           class="btn btn-dark @nextDisabled">
            İleri
        </a>
    }
</div>
```

---

### Özet ve İpuçları
- **Performans:** `Skip` ve `Take` kullanmak, tüm tabloyu hafızaya (RAM) çekmek yerine sadece ihtiyacınız olan 10 kaydı çekmenizi sağlar.
- **Sıralama:** Sayfalama yaparken `OrderBy` kullanmak zorunludur. Aksi takdirde veriler her sayfada farklı sırada gelebilir.
- **Geliştirme:** Bu sistemi projelerinize dahil ederek büyük veri setlerini saniyeler içinde yönetebilirsiniz.

---
*Hazırlayan: Blog Yönetim Sistemi*

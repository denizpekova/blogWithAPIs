# IdentityServer (Duende) Kurulum Yol Haritası

Bu doküman, Blog API projesine kimlik doğrulama (Authentication) ve yetkilendirme (Authorization) mekanizmalarının entegre edilmesini kapsar.

## FAZ 1: Paket Kurulumu ve Temel Yapılandırma
IdentityServer için gerekli temel kütüphanelerin eklenmesi ve başlangıç ayarlarının yapılması.

- [ ] **NuGet Paketleri:** `Duende.IdentityServer.AspNetIdentity` paketinin kurulması.
- [ ] **Config Sınıfı:** `Config.cs` adında bir sınıf oluşturularak Client'lar, API Scope'lar ve Identity Resource'ların tanımlanması.
- [ ] **Program.cs Kaydı:** `AddIdentityServer()` servisinin eklenmesi.

## FAZ 2: Veritabanı ve Kullanıcı Yönetimi Entegrasyonu
IdentityServer'ın ASP.NET Core Identity (Kullanıcı tabloları) ile birleştirilmesi.

- [ ] **Identity Sınıfları:** `AppUser` ve `AppRole` sınıflarının oluşturulması.
- [ ] **DbContext Güncellemesi:** `Context` sınıfının `IdentityDbContext`'den türetilmesi ve migration yapılması.
- [ ] **Kullanıcı Servisleri:** IdentityServer'ın bu kullanıcıları tanıması için `AddAspNetIdentity<AppUser>()` konfigürasyonu.

## FAZ 3: API Katmanının Korunması (Protection)
API endpoint'lerimizin sadece geçerli bir Token (JWT) ile erişilebilir hale getirilmesi.

- [ ] **Authentication Middleware:** API projesine `AddAuthentication` ve `AddJwtBearer` eklenmesi.
- [ ] **Authorize Özniteliği:** Controller'lara veya metotlara `[Authorize]` eklenerek koruma altına alınması.
- [ ] **Global Hata Yönetimi Güncellemesi:** 401 (Unauthorized) hatalarının Faz 3'teki Handler tarafından yakalanması.

## FAZ 4: Token Alma ve Test (Postman)
Sistemin test edilmesi ve Token akışlarının (Grant Types) doğrulanması.

- [ ] **Token Endpoint Testi:** Postman üzerinden `connect/token` adresine istek atılarak JWT alınması.
- [ ] **Bearer Token Kullanımı:** Alınan token ile korumalı API'lere istek atılması.
- [ ] **Giriş ve Kayıt İşlemleri:** Yeni kullanıcı kaydı ve giriş için gerekli API uçlarının yazılması.

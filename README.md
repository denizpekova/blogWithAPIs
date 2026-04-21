# 🌌 Core Blog Engine: The 2026 Reference Implementation

![Tech Stack](https://img.shields.io/badge/Architecture-Clean_Architecture-blueviolet?style=for-the-badge&logo=architecture)
![Backend](https://img.shields.io/badge/.NET_10.0-ASP.NET_Core_Web_API-512BD4?style=for-the-badge&logo=dotnet)
![Security](https://img.shields.io/badge/IdentityServer-OpenID_Connect-FF69B4?style=for-the-badge&logo=auth0)
![Database](https://img.shields.io/badge/MSSQL_Server-Enterprise_Ready-CC2927?style=for-the-badge&logo=microsoft-sql-server)

> **C# .NET 10** tabanlı, ultra-performanslı ve modüler bir Blog API ekosistemi. Bu proje, "klasik" bir blog yapısını günümüzün en ileri mühendislik standartlarıyla buluşturan, **IdentityServer** ile zırhlandırılmış bir mimari şaheserdir.

---

## 🏛 Mimari Vizyon (The Structural Excellence)

Bu sistem, **Domain-Driven Design (DDD)** ve **CQRS** prensipleri üzerine inşa edilmiştir. Gereksiz karmaşadan arındırılmış, ancak ölçeklenebilirlik noktasında tavizsiz bir yapı sunar.

- **Clean Architecture:** Bağımlılıkların merkeze (Domain) doğru aktığı, test edilebilir ve sürdürülebilir yapı.
- **IdentityServer Integration:** Admin ve kullanıcı yetkilendirmelerinde endüstri standardı olan OIDC/OAuth2 protokolleri ile tam güvenlik.
- **Fluent Validation & MediatR:** İş mantığı ve validasyon süreçlerinde yüksek akışkanlık ve düşük bağımlılık.

---

## 💎 Temel Özellikler (Core Engineering)

### 📝 Blog Yönetimi (The Content Core)
Blog operasyonları, en düşük gecikme süresi ve en yüksek veri bütünlüğü ile kurgulanmıştır:

- **Blog Create:** Zengin içerik desteği ve SEO uyumlu slug üretimi.
- **Blog Read:** Yüksek performanslı listeleme ve detay görünümleri (Pagination & Filtering).
- **Blog Update:** Dinamik içerik revizyonları.
- **Blog Delete:** Güvenli ve izlenebilir veri silme süreçleri (Soft/Hard Delete support).

### 🔐 Admin & Güvenlik (Sentinel Protocol)
- **Admin Authentication:** IdentityServer ile tokenize edilmiş giriş mekanizması.
- **Role-Based Access Control (RBAC):** Sadece yetkili mimarların (Admin) içeriğe müdahale edebildiği katmanlı güvenlik sistemi.
- **JWT Protection:** Her API çağrısında pürüzsüz ve güvenli doğrulama.

---

## 🚀 Teknoloji Stack'i (The 2026 Toolkit)

| Katman | Teknoloji | Amaç |
| :--- | :--- | :--- |
| **Backend** | .NET Core Web API (v10) | Core Logic |
| **Auth** | IdentityServer / Duende | Security & Identity |
| **ORM** | Entity Framework Core | Database Mapping |
| **Storage** | Microsoft SQL Server (MSSQL) | Enterprise Data |
| **Patterns** | CQRS + MediatR | Scaling & Flow |
| **Validation** | FluentValidation | Data Integrity |

---

## 🛠 Kurulum (Assembly Guide)

1.  **Repository'i Klonlayın:**
    ```bash
    git clone https://github.com/username/blog-api-core.git
    ```
2.  **Bağımlılıkları Yükleyin:**
    ```bash
    dotnet restore
    ```
3.  **Veritabanı Migrasyonlarını Uygulayın:**
    ```bash
    dotnet ef database update
    ```
4.  **Sistemi Ateşleyin:**
    ```bash
    dotnet run --project BlogAPI.WebAPI
    ```

---

## 🎨 UI/UX Notu: Glassmorphism 2.0
Bu API, **Glassmorphism 2.0** prensipleriyle tasarlanmış modern frontend yapıları için optimize edilmiştir. API response yapıları, pürüzsüz animasyonlar ve anlık veri senkronizasyonu için tasarlanmıştır.

---

<p align="center">
  <i>"Kod, sadece bir talimat seti değil; dijital bir sanattır."</i><br>
  <b>2026 © Core Architecture Collective</b>
</p>
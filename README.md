# 🛡️ Sentinel Engine: High-Performance .NET 10 Blog API

![Backend](https://img.shields.io/badge/.NET_10.0-Sentinel_Core-512BD4?style=for-the-badge&logo=dotnet)
![Architecture](https://img.shields.io/badge/Architecture-N--Tier_/_Clean-blueviolet?style=for-the-badge&logo=architecture)
![Security](https://img.shields.io/badge/Security-IdentityServer_7-orange?style=for-the-badge)

> **Sentinel Engine**, modern yazılım mimarisinin zirvesini temsil eden, yüksek performanslı ve sarsılmaz bir blog API servisidir. **.NET 10**'un gücüyle inşa edilen bu sistem, karmaşık veri süreçlerini en üst düzey güvenlik protokolleriyle yönetir.

---

## 🏛️ Mimari Felsefe (The Structural Vision)

Sistem, "Sıfır Hata, Maksimum Güvenlik" ilkesiyle **N-Tier Architecture (Katmanlı Mimari)** prensiplerine sadık kalarak inşa edilmiştir:

- **Sentinel Core (The API):** Uygulamanın dünyaya açılan kapısı. Deployment konfigürasyonları ve Controller yapılarını barındırır.
- **Sentinel Business (The Logic):** Tüm iş mantığı ve validasyonların yönetildiği merkezi katman. 
- **Sentinel DataAccess (The Persistence):** EF Core 9+ ile SQLite veritabanı iletişimi ve Repository pattern uygulamaları.
- **Sentinel Entity (The Domain):** Tüm sistem genelinde kullanılan varlıklar (Entities) ve DTO'lar.

---

## 🛡️ Siber Güvenlik & Mimari Özellikler

Proje üzerinde yapılan son geliştirmelerle sistem "Pentagon" standartlarında bir güvenliğe kavuşturulmuştur:

- **🔒 Siber Kalkan (Rate Limiting):** API uç noktaları bot saldırılarına ve Brute Force girişimlerine karşı saniyede 10 istek sınırı ile korunmaktadır.
- **🛡️ Audit Logging (Siber Kayıt):** Sistemdeki tüm kritik hareketler (Giriş, Kayıt, Silme, Güncelleme) IP adresi ve kullanıcı bilgisiyle beraber veritabanına loglanmaktadır.
- **🌐 Domain Independence:** Backend kodları hiçbir domain veya porta bağımlı değildir. Tüm adresler `appsettings.json` üzerinden dinamik olarak yönetilir.

## 📁 Proje Yapısı

- **blogWithAPI:** Core API projesi (Controllers, Filters, IdentityServer).
- **blogWithAPI.BusinessLayer:** İş mantığı ve loglama servisleri (AuditManager).
- **blogWithAPI.DataAccessLayer:** Veritabanı katmanı ve Migrationlar (SQLite).
- **blogWithAPI.Entity:** Ortak nesne modelleri (Blog, AuditLog, AppUser).
- **frontend/blog:** Next.js tabanlı, siber estetikli kullanıcı arayüzü.

## 🛠️ Kurulum ve Yayınlama

### Backend (Windows İçin):
```bash
dotnet publish -c Release -r win-x64 --self-contained true
```
Bu komut sonunda oluşan paketi sunucuya atıp `appsettings.json` içindeki domainleri güncellemeniz yeterlidir.

### Veritabanı Güncelleme:
```bash
dotnet ef database update --project blogWithAPI.DataAccessLayer --startup-project blogWithAPI
```

---
*Bu proje modern siber güvenlik mimarileri ve yüksek performanslı .NET teknolojileri ile inşa edilmiştir.*
```bash
dotnet run
```

---

## 🌐 Dağıtım (Deployment)
API, IIS ve Plesk ortamları için optimize edilmiştir. Üretim paketi hazırlamak için:
```bash
dotnet publish -c Release -o ./publish
```

---

<p align="center">
  <i>"Temiz kod, sarsılmaz bir mimariyle başlar."</i><br>
  <b>2026 © <b>
</p>

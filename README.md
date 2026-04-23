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

## 🛡️ Güvenlik ve Kimlik Yönetimi (The Shield)

Sentiel Engine, enterprise düzeyde güvenlik protokollerini standart olarak sunar:
- **IdentityServer Entegrasyonu:** Duende IdentityServer ile merkezi kimlik yönetimi.
- **JWT Bearer Protection:** Tüm API uç noktaları JSON Web Token ile zırhlandırılmıştır.
- **Result Pattern:** Tüm operasyonel yanıtlar `IDataResult` ve `IResult` standartlarıyla döner, hata yönetimi otomatiktir.

---

## 🚀 Özellikler (Core Intel)

- [x] **Advanced Hashing:** SHA256 tabanlı güvenli şifreleme mekanizması.
- [x] **Markdown Engine:** Veritabanında zengin teknik içerik saklama yeteneği.
- [x] **Dynamic Routing:** SEO dostu veri çekme ve yönetme uç noktaları.
- [x] **Global Error Handler:** Merkezi hata yakalama ve loglama altyapısı.

---

## 🛠️ Kurulum Rehberi (Assembly Guide)

### 1. Depoyu Klonlayın
```bash
git clone https://github.com/denizpekova/blogWithAPIs.git
cd blogWithAPIs
```

### 2. Bağımlılıkları Yükleyin ve Derleyin
```bash
cd blogWithAPI
dotnet restore
dotnet build
```

### 3. Uygulamayı Çalıştırın
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

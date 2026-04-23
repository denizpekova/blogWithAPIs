# 🛡️ Sentinel & Aurora: The 2026 Full-Stack Symphony

![Architecture](https://img.shields.io/badge/Architecture-Monorepo_/_N--Tier-blueviolet?style=for-the-badge&logo=architecture)
![Backend](https://img.shields.io/badge/.NET_10.0-Sentinel_Core-512BD4?style=for-the-badge&logo=dotnet)
![Frontend](https://img.shields.io/badge/Next.js_16-Aurora_UI-000000?style=for-the-badge&logo=next.js)
![Design](https://img.shields.io/badge/Design-Glassmorphism_2.0-orange?style=for-the-badge)

> **Sentinel & Aurora**, modern yazılım mimarisinin zirvesini temsil eden, **.NET 10**'un sarsılmaz gücü ile **Next.js 16**'nın büyüleyici estetiğinin birleştiği dijital bir başyapıttır. Bu ekosistem, karmaşık backend süreçlerini (Sentinel) ve nefes kesen kullanıcı deneyimini (Aurora) tek bir monorepo senfonisinde buluşturur.

---

## 🏛️ Mimari Felsefe (The Structural Vision)

Sistem, "Sıfır Hata, Maksimum Estetik" ilkesiyle iki ana güç odağı üzerine inşa edilmiştir:

### 🛡️ Sentinel Engine (The Backend)
Arka plandaki koruyucu güç. .NET 10 üzerine inşa edilen bu katman, veriyi sadece işlemez; onu korur ve en saf haliyle frontend'e aktarır.
- **N-Tier Separation:** DataAccess, Business ve Entity katmanları arasında keskin ayrım.
- **Result Pattern Implementation:** Tüm yanıtlar `IDataResult` ve `IResult` standartlarıyla, frontend için öngörülebilir bir yapıda döner.
- **Sentinel Auth:** Duende IdentityServer tabanlı, JWT ile zırhlandırılmış RBAC (Role-Based Access Control) mekanizması.

### 🎨 Aurora UI (The Frontend)
Ön plandaki ışık. Next.js 16 (App Router) ile kurgulanan bu katman, dijital bir camın arkasındaymışsınız hissi veren **Glassmorphism 2.0** tasarım dilini kullanır.
- **Ultra-Glassmorphism:** `backdrop-filter: blur(40px)` ve pürüzsüz `saturate` değerleriyle üst düzey derinlik.
- **Aurora Animations:** Framer Motion ile kurgulanmış, `stiffness: 100` ve `damping: 20` değerlerine sahip kusursuz yay animasyonları.
- **Global Context Architecture:** Dil (TR/EN) ve Auth durumları uygulama genelinde akışkan bir şekilde yönetilir.

---

## 📂 Proje Yapısı (Project Ecosystem)

```text
blogWithAPI/
├── 🛡️ Sentinel Core (Backend)
│   ├── blogWithAPI/                # API Controllers & Deployment Config
│   ├── blogWithAPI.BusinessLayer/  # Logic, Result Pattern & Validation
│   ├── blogWithAPI.DataAccessLayer/# EF Core, SQLite & Repositories
│   └── blogWithAPI.Entity/         # Entities, DTOs & Global Models
│
└── 🎨 Aurora UI (Frontend)
    ├── src/app/                    # Next.js Pages (Auth, Admin, Blog)
    ├── src/components/             # Premium Glassmorphic UI Components
    ├── src/contexts/               # Global State (Language & Auth)
    └── src/services/               # High-Performance API Integration
```

---

## 🚀 Teknoloji Stack'i (The 2026 Toolkit)

| Bileşen | Teknoloji | Amaç |
| :--- | :--- | :--- |
| **Runtime** | .NET 10.0 / Node.js 22+ | Modern Execution Environment |
| **Framework** | Next.js 16 (React 19) | Frontend Symphony |
| **Security** | Duende IdentityServer 7 | Enterprise Security |
| **Storage** | Entity Framework Core / SQLite | Persistence & Speed |
| **Animation** | Framer Motion | Fluid Motion Design |
| **Styling** | Vanilla CSS / Glassmorphism | Aesthetic Excellence |

---

## 🛠️ Kurulum Rehberi (Assembly Guide)

### 1. Ekosistemi Klonlayın
```bash
git clone https://github.com/denizpekova/blogWithAPIs.git
cd blogWithAPIs
```

### 2. Sentinel Core (Backend) Aktivasyonu
```bash
cd blogWithAPI
dotnet build
dotnet run
```

### 3. Aurora UI (Frontend) Aktivasyonu
```bash
cd frontend/blog
npm install
npm run dev
```

---

## 🌐 Dağıtım & Yayın (Deployment)
Sistem, IIS ve Plesk ortamları için optimize edilmiş `web.config` yapılandırmalarına sahiptir. Üretim build'leri için:
- **Backend:** `dotnet publish -c Release`
- **Frontend:** `npm run build`

---

<p align="center">
  <i>"Kod sadece bir araç değil, dijital bir sanattır."</i><br>
  <b>2026 © MT Development<b>
</p>

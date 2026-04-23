# Modern Blog Platform (Next.js)

Bu proje, premium tasarımı ve modern kullanıcı arayüzü ile Node.js (Next.js) kullanılarak geliştirilmiştir.

## Özellikler

- **Giriş / Kayıt Ol:** Modern form tasarımları ve doğrulama süreçleri.
- **Main Page:** Dinamik blog gönderileri listesi ve etkileyici Hero alanı.
- **E-posta Doğrulama:** Kayıt sonrası yönlendirme ve başarı sayfaları.
- **Şifre Değiştirme:** Güvenli şifre sıfırlama akışı.
- **C# API Entegrasyonu:** `src/services/api.js` üzerinden tüm backend iletişimini yönetir.

## Backend Bağlantısı (C# API)

Projeyi gerçek C# API'nıza bağlamak için:

1. `src/services/api.js` dosyasına gidin.
2. `IS__MOCK = true` olan değişkeni `false` yapın.
3. `NEXT_PUBLIC_API_URL` değişkenini API adresinizle güncelleyin.

## Çalıştırma

Terminalde şu komutları çalıştırın:

```bash
npm install
npm run dev
```

Uygulama `http://localhost:3000` adresinde çalışacaktır.

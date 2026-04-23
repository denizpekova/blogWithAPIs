'use client';
import Link from 'next/link';
import '@/app/auth/auth.css';

export default function Verified() {
  return (
    <div className="auth-container animate-fade-in">
      <div className="auth-card glass text-center">
        <div className="status-icon success">✅</div>
        <h2>Hesabınız Doğrulandı!</h2>
        <p className="auth-subtitle">
          Tebrikler! E-posta adresiniz başarıyla doğrulandı. Artık blog içeriklerini takip edebilir ve yorum yapabilirsiniz.
        </p>
        
        <Link href="/auth/login" className="btn-primary" style={{ width: '100%', marginTop: '1.5rem' }}>
          Giriş Yap ve Başla
        </Link>
      </div>
    </div>
  );
}

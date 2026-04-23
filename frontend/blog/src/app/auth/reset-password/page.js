'use client';
import { useState } from 'react';
import Link from 'next/link';
import '@/app/auth/auth.css';

export default function ResetPassword() {
  const [step, setStep] = useState(1); // 1: E-posta İste, 2: Yeni Şifre
  const [loading, setLoading] = useState(false);

  const handleRequest = (e) => {
    e.preventDefault();
    setLoading(true);
    setTimeout(() => {
      setLoading(false);
      setStep(2);
    }, 1200);
  };

  const handleChange = (e) => {
    e.preventDefault();
    setLoading(true);
    setTimeout(() => {
      setLoading(false);
      alert('Şifreniz başarıyla güncellendi!');
      window.location.href = '/auth/login';
    }, 1200);
  };

  return (
    <div className="auth-container animate-fade-in">
      <div className="auth-card glass">
        {step === 1 ? (
          <>
            <h2>Şifremi Unuttum</h2>
            <p className="auth-subtitle">E-posta adresinizi girin, size bir sıfırlama kodu gönderelim.</p>
            <form onSubmit={handleRequest} className="auth-form">
              <div className="form-group">
                <label>E-posta Adresi</label>
                <input type="email" placeholder="e-posta@örnek.com" required />
              </div>
              <button type="submit" className="btn-primary auth-submit" disabled={loading}>
                {loading ? 'Gönderiliyor...' : 'Sıfırlama Bağlantısı Gönder'}
              </button>
            </form>
          </>
        ) : (
          <>
            <h2>Yeni Şifre Belirle</h2>
            <p className="auth-subtitle">Lütfen hesabınız için yeni ve güvenli bir şifre oluşturun.</p>
            <form onSubmit={handleChange} className="auth-form">
              <div className="form-group">
                <label>Yeni Şifre</label>
                <input type="password" placeholder="••••••••" required />
              </div>
              <div className="form-group">
                <label>Şifre Tekrar</label>
                <input type="password" placeholder="••••••••" required />
              </div>
              <button type="submit" className="btn-primary auth-submit" disabled={loading}>
                {loading ? 'Güncelleniyor...' : 'Şifreyi Güncelle'}
              </button>
            </form>
          </>
        )}
        
        <div className="auth-footer">
          <Link href="/auth/login">Giriş sayfasına dön</Link>
        </div>
      </div>
    </div>
  );
}

'use client';
import Link from 'next/link';
import '@/app/auth/auth.css';

export default function Verify() {
  return (
    <div className="auth-container animate-fade-in">
      <div className="auth-card glass text-center">
        <div className="status-icon info">✉️</div>
        <h2>E-posta Doğrulama</h2>
        <p className="auth-subtitle">
          Lütfen kayıt olduğunuz e-posta adresini kontrol edin. Size bir doğrulama bağlantısı gönderdik.
        </p>
        
        <div className="verify-actions">
          <button className="btn-secondary">Tekrar Gönder</button>
          <Link href="/auth/verified" className="btn-primary">Doğrulamayı Onayla (Demo)</Link>
        </div>

        <div className="auth-footer">
          <Link href="/auth/login">Giriş sayfasına dön</Link>
        </div>
      </div>
    </div>
  );
}

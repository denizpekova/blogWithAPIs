'use client';
import { useState } from 'react';
import Link from 'next/link';
import '@/app/auth/auth.css';

export default function Register() {
  const [formData, setFormData] = useState({ name: '', email: '', password: '' });
  const [loading, setLoading] = useState(false);

  const handleSubmit = (e) => {
    e.preventDefault();
    setLoading(true);
    setTimeout(() => {
      setLoading(false);
      window.location.href = '/auth/verify';
    }, 1500);
  };

  return (
    <div className="auth-container animate-fade-in">
      <div className="auth-card glass">
        <h2>Kayıt Ol</h2>
        <p className="auth-subtitle">Blog dünyasına katılmak için hemen profilini oluştur.</p>
        
        <form onSubmit={handleSubmit} className="auth-form">
          <div className="form-group">
            <label>Ad Soyad</label>
            <input 
              type="text" 
              placeholder="Ahmet Yılmaz" 
              required 
              value={formData.name}
              onChange={(e) => setFormData({...formData, name: e.target.value})}
            />
          </div>

          <div className="form-group">
            <label>E-posta Adresi</label>
            <input 
              type="email" 
              placeholder="e-posta@örnek.com" 
              required 
              value={formData.email}
              onChange={(e) => setFormData({...formData, email: e.target.value})}
            />
          </div>
          
          <div className="form-group">
            <label>Şifre</label>
            <input 
              type="password" 
              placeholder="••••••••" 
              required 
              value={formData.password}
              onChange={(e) => setFormData({...formData, password: e.target.value})}
            />
          </div>

          <button type="submit" className="btn-primary auth-submit" disabled={loading}>
            {loading ? 'Yükleniyor...' : 'Kayıt İşlemini Tamamla'}
          </button>
        </form>

        <div className="auth-footer">
          <span>Zaten üye misiniz?</span>
          <Link href="/auth/login">Giriş Yap</Link>
        </div>
      </div>
    </div>
  );
}

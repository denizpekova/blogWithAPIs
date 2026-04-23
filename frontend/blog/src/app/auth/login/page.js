'use client';
import { useState } from 'react';
import Link from 'next/link';
import { motion } from 'framer-motion';
import { Mail, Lock, LogIn } from 'lucide-react';
import { useLanguage } from '@/contexts/LanguageContext';
import '@/app/auth/auth.css';

export default function Login() {
  const [formData, setFormData] = useState({ email: '', password: '' });
  const [loading, setLoading] = useState(false);
  const { lang } = useLanguage();

  const content = {
    en: {
      title: "Welcome back.",
      subtitle: "Continue your creative journey.",
      emailLabel: "Intel Email",
      passLabel: "Secure Key",
      forgot: "Lost your key?",
      button: "Sign In",
      loading: "Authenticating...",
      alert: "Access Granted. Welcome back."
    },
    tr: {
      title: "Otoriteye Dönüş.",
      subtitle: "Sistemin kontrolünü yeniden devralın.",
      emailLabel: "Sistem E-postası",
      passLabel: "Güvenlik Anahtarı",
      forgot: "Anahtarınızı mı kaybettiniz?",
      button: "Matrise Giriş Yap",
      loading: "Doğrulanıyor...",
      alert: "Erişim İzni Verildi."
    }
  };

  const t = content[lang];

  const handleSubmit = (e) => {
    e.preventDefault();
    setLoading(true);
    setTimeout(() => {
      setLoading(false);
      alert(t.alert);
    }, 1500);
  };

  return (
    <div className="auth-container">
      <motion.div 
        initial={{ opacity: 0, scale: 0.9, y: 20 }}
        animate={{ opacity: 1, scale: 1, y: 0 }}
        transition={{ type: "spring", stiffness: 100, damping: 20 }}
        className="auth-card glass-panel"
      >
        <h2 className="text-gradient epic-title">{t.title}</h2>
        <p className="auth-subtitle">{t.subtitle}</p>
        
        <form onSubmit={handleSubmit} className="auth-form">
          <div className="form-group">
            <label>{t.emailLabel}</label>
            <input 
              type="email" 
              placeholder="architect@matrix.net" 
              required 
              value={formData.email}
              onChange={(e) => setFormData({...formData, email: e.target.value})}
            />
          </div>
          
          <div className="form-group">
            <label>{t.passLabel}</label>
            <input 
              type="password" 
              placeholder="••••••••" 
              required 
              value={formData.password}
              onChange={(e) => setFormData({...formData, password: e.target.value})}
            />
            <Link href="/auth/reset-password" title="Restore Access" className="forgot-link">{t.forgot}</Link>
          </div>

          <button type="submit" className="btn-premium auth-submit-btn" disabled={loading}>
            {loading ? t.loading : t.button}
          </button>
        </form>
      </motion.div>
    </div>
  );
}

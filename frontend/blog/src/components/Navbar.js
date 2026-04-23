'use client';
import { useEffect, useState } from 'react';
import { useRouter } from 'next/navigation';
import Link from 'next/link';
import { useLanguage } from '@/contexts/LanguageContext';
import { apiService } from '@/services/api';
import './Navbar.css';

export default function Navbar() {
  const { lang, setLang } = useLanguage();
  const [user, setUser] = useState(null);
  const router = useRouter();

  useEffect(() => {
    const checkUser = async () => {
      const data = await apiService.getCurrentUser();
      // API Result wrapper kullandığı için data.data kontrolü yapıyoruz
      if (data && data.isSuccess) {
        setUser(data.data);
      } else {
        setUser(null);
      }
    };
    checkUser();
  }, []);

  const handleLogout = async () => {
    localStorage.removeItem('token');
    setUser(null);
    router.push('/');
    router.refresh();
  };

  const content = {
    tr: {
      home: "Ana Sayfa",
      admin: "Yönetim",
      login: "Giriş Yap",
      logout: "Çıkış Yap"
    },
    en: {
      home: "Home",
      admin: "Admin",
      login: "Login",
      logout: "Logout"
    }
  };

  const t = content[lang] || content.tr;

  return (
    <nav className="navbar">
      <div className="navbar-container glass-panel">
        <Link href="/" className="logo text-gradient">
          bloged<span>.</span>
        </Link>
        <div className="nav-links">
          <Link href="/" className="nav-link">{t.home}</Link>
          
          {user && user.role === 'Admin' && (
            <Link href="/admin" className="nav-link">{t.admin}</Link>
          )}

          <div className="lang-selector-nav">
            <button className={`lang-btn-nav ${lang === 'tr' ? 'active' : ''}`} onClick={() => setLang('tr')}>TR</button>
            <span className="lang-divider-nav">/</span>
            <button className={`lang-btn-nav ${lang === 'en' ? 'active' : ''}`} onClick={() => setLang('en')}>EN</button>
          </div>
          
          <div className="nav-auth">
            {user ? (
               <button onClick={handleLogout} className="btn-premium-outline" style={{padding: '8px 16px', borderRadius: '12px'}}>{t.logout}</button>
            ) : (
              <Link href="/auth/login" className="btn-premium">{t.login}</Link>
            )}
          </div>
        </div>
      </div>
    </nav>
  );
}

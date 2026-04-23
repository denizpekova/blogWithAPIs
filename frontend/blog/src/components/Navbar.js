'use client';
import Link from 'next/link';
import { useLanguage } from '@/contexts/LanguageContext';
import './Navbar.css';

export default function Navbar() {
  const { lang, setLang } = useLanguage();

  const content = {
    tr: {
      home: "Ana Sayfa",
      admin: "Yönetim",
      login: "Giriş Yap"
    },
    en: {
      home: "Home",
      admin: "Admin",
      login: "Login"
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
          <Link href="/admin" className="nav-link">{t.admin}</Link>
          <div className="lang-selector-nav">
            <button className={`lang-btn-nav ${lang === 'tr' ? 'active' : ''}`} onClick={() => setLang('tr')}>TR</button>
            <span className="lang-divider-nav">/</span>
            <button className={`lang-btn-nav ${lang === 'en' ? 'active' : ''}`} onClick={() => setLang('en')}>EN</button>
          </div>
          <div className="nav-auth">
            <Link href="/auth/login" className="btn-premium">{t.login}</Link>
          </div>
        </div>
      </div>
    </nav>
  );
}

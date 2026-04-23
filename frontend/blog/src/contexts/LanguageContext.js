'use client';
import { createContext, useContext, useState, useEffect } from 'react';

const LanguageContext = createContext();

export function LanguageProvider({ children }) {
  const [lang, setLang] = useState('tr');

  useEffect(() => {
    const saved = localStorage.getItem('bloged_lang');
    if(saved) setLang(saved);
  }, []);

  const changeLang = (l) => {
    setLang(l);
    localStorage.setItem('bloged_lang', l);
  };

  return (
    <LanguageContext.Provider value={{ lang, setLang: changeLang }}>
      {children}
    </LanguageContext.Provider>
  );
}

export const useLanguage = () => useContext(LanguageContext);

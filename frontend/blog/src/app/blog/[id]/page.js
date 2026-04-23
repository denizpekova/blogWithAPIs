'use client';
import { useEffect, useState } from 'react';
import { useParams } from 'next/navigation';
import { motion } from 'framer-motion';
import { Share2, Bookmark, Clock, User, ArrowLeft, Heart } from 'lucide-react';
import Link from 'next/link';
import { apiService } from '@/services/api';
import './blog.css';

const spring = {
  type: "spring",
  stiffness: 100,
  damping: 20
};

export default function ArchitectBlogDetail() {
  const { id } = useParams();
  const [post, setPost] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchPost = async () => {
      try {
        const data = await apiService.getById('/posts', id);
        setPost(data);
      } catch (err) {
        console.error('Fetch error:', err);
      } finally {
        setLoading(false);
      }
    };
    fetchPost();
  }, [id]);

  if (loading) {
    return (
      <div className="visionary-loader-container">
        <motion.div 
          animate={{ rotate: 360, scale: [1, 1.2, 1] }}
          transition={{ duration: 2, repeat: Infinity, ease: "linear" }}
          className="visionary-loader"
        />
      </div>
    );
  }

  if (!post) {
    return (
      <div className="article-container text-center pt-32">
         <h2 className="epic-title">Signal Lost.</h2>
      </div>
    );
  }

  return (
    <motion.article 
      initial={{ opacity: 0 }}
      animate={{ opacity: 1 }}
      exit={{ opacity: 0 }}
      transition={{ duration: 0.8 }}
      className="architect-blog-detail"
    >
      {/* 2026 Dynamic Mesh Gradients Background */}
      <div className="dynamic-mesh-background">
        <div className="mesh-orb orb-1"></div>
        <div className="mesh-orb orb-2"></div>
        <div className="mesh-orb orb-3"></div>
        <div className="mesh-noise"></div>
      </div>

      <header className="article-header">
        <div className="article-container">
          <Link href="/" className="back-badge">
            <ArrowLeft size={16} /> <span>Return to Nexus</span>
          </Link>
          
          <motion.div 
            initial={{ y: 30, opacity: 0 }}
            animate={{ y: 0, opacity: 1 }}
            transition={{ ...spring, delay: 0.1 }}
            className="meta-cluster"
          >
             <span className="crystal-badge">SYSTEM ARCHITECTURE</span>
             <span className="meta-info"><Clock size={14} /> {post.date}</span>
             <span className="meta-info"><User size={14} /> {post.author}</span>
          </motion.div>

          <motion.h1 
            initial={{ y: 30, opacity: 0 }}
            animate={{ y: 0, opacity: 1 }}
            transition={{ ...spring, delay: 0.2 }}
            className="epic-title"
          >
            {post.title}
          </motion.h1>
        </div>

        <motion.div 
          initial={{ y: 50, opacity: 0, scale: 0.95 }}
          animate={{ y: 0, opacity: 1, scale: 1 }}
          transition={{ ...spring, delay: 0.3 }}
          className="article-hero-media"
        >
          <img src={post.image} alt={post.title} className="hero-img" />
          <div className="hero-img-overlay"></div>
        </motion.div>
      </header>

      <div className="article-container layout-with-sidebar">
        <motion.aside 
          initial={{ x: -30, opacity: 0 }}
          animate={{ x: 0, opacity: 1 }}
          transition={{ ...spring, delay: 0.4 }}
          className="article-sidebar"
        >
          <div className="sticky-actions">
            <button className="action-btn"><Heart size={20} /></button>
            <button className="action-btn"><Share2 size={20} /></button>
            <button className="action-btn"><Bookmark size={20} /></button>
          </div>
        </motion.aside>

        <motion.main 
          initial={{ y: 40, opacity: 0 }}
          animate={{ y: 0, opacity: 1 }}
          transition={{ ...spring, delay: 0.5 }}
          className="article-body"
        >
          <div className="rich-text-content">
            <p className="lead-paragraph">
              {post.summary}
            </p>

            <h2 className="section-heading">Geleceğin Mimarisini İnşa Etmek</h2>
            <p>
              Embracing <strong>Glassmorphism 2.0</strong> necessitates leaving flat design in the past. 
              Geleneksel web siteleri sadece bilgi verir, ancak bu yapılar duygusal bir zekaya sahiptir.
              Her tıklama, her kaydırma hareketi WebGPU üzerinden işlenerek kusursuz bir tepki mekanizması oluşturur.
            </p>

            <blockquote className="visionary-quote">
              "Arayüzlerimiz artık ekranlarda değil, kullanıcıların zihinlerinde var olan uzamsal alanlardır."
            </blockquote>

            <h2 className="section-heading">Matematiksel Performans</h2>
            <p>
              Tasarım dilimizdeki "Inner Glow" tekniği, %5 beyaz opaklık ile nesnelere fütüristik bir hacim 
              kazandırır. Bu sadece bir gölge efekti değil, dijital varlığın evrenle çarpışmasından doğan bir enerji 
              patlamasıdır.
            </p>

            <div className="bento-box">
              <h3 className="bento-title">Sistemin Temel Prensipleri</h3>
              <ul className="bento-list">
                <li>Backdrop Filter Blur: 40px, Saturate: %150</li>
                <li>Framer Motion Yay Dinamiği: Stiffness 100, Damping 20</li>
                <li>Dynamic Mesh Animasyonları: Renklerin sürekli akışı</li>
              </ul>
            </div>
            
            <p>
              Tüm bu elementlerin mükemmel bir uyum içinde çalışabilmesi için CSS modüllerini ve DOM
              manipülasyonlarını tamamen asenkronize ettik. 2026'da "yükleme süresi" kavramı artık 
              tasarımın ayrılmaz bir deneyimidir.
            </p>
          </div>
        </motion.main>
      </div>
    </motion.article>
  );
}

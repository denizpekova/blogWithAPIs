'use client';
import { useEffect, useState } from 'react';
import Link from 'next/link';
import { motion, AnimatePresence } from 'framer-motion';
import { ArrowRight, Clock, User, ChevronRight, Zap } from 'lucide-react';
import { apiService } from '@/services/api';
import { useLanguage } from '@/contexts/LanguageContext';
import './home.css';

const spring = {
  type: "spring",
  stiffness: 100,
  damping: 20
};

export default function Home() {
  const [posts, setPosts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [currentPage, setCurrentPage] = useState(1);
  const { lang } = useLanguage();
  const postsPerPage = 5;

  const content = {
    tr: {
      heroTitle: <>Dijital İfadenin <br /> Geleceği.</>,
      heroDesc: "Modern web çağında üst düzey tasarım, nöral mimari ve akıcı kullanıcı deneyimlerinin kesişimini keşfedin.",
      latest: "Son Yazılar",
      prevBtn: "Önceki Bölüm",
      nextBtn: "Sonraki Bölüm",
      tag: "Yapay Zeka & Tasarım",
      category: "TEKNOLOJİ & SİBER"
    },
    en: {
      heroTitle: <>The Future of <br /> Digital Expression.</>,
      heroDesc: "Explore the intersection of high-end design, neural architecture, and fluid user experiences in the modern web era.",
      latest: "Latest Intel",
      prevBtn: "Previous Sector",
      nextBtn: "Next Sector",
      tag: "AI & Design",
      category: "TECH & CYBER"
    }
  };

  const t = content[lang] || content.tr;

  useEffect(() => {
    const fetchPosts = async () => {
      try {
        const response = await apiService.get('/posts');
        // API bir Result objesi döndüğü için içindeki 'data' kısmını alıyoruz
        if (response && response.data) {
          // API alan isimlerini (imageUrl, createdDate) Frontend isimlerine (image, date) çeviriyoruz
          const mappedData = response.data.map(item => ({
            ...item,
            image: item.imageUrl || item.image,
            date: item.createdDate ? new Date(item.createdDate).toLocaleDateString() : item.date,
            summary: item.content || item.summary,
            category: t.category
          }));
          setPosts(mappedData);
        } else if (Array.isArray(response)) {
          setPosts(response);
        }
      } catch (err) {
        console.error('Fetch error:', err);
      } finally {
        setLoading(false);
      }
    };
    fetchPosts();
  }, [lang]);

  return (
    <div className="home-wrapper">
      <section className="hero-section">
        <div className="container">
          
          <motion.h1 
            initial={{ opacity: 0, y: 40 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ ...spring, delay: 0.2 }}
            className="hero-title text-gradient"
          >
            {t.heroTitle}
          </motion.h1>
          
          <motion.p 
            initial={{ opacity: 0, y: 40 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ ...spring, delay: 0.3 }}
            className="hero-description"
          >
            {t.heroDesc}
          </motion.p>
          
        </div>
      </section>

      <section className="feed-section">
        <div className="container">
          <motion.div 
            initial={{ opacity: 0 }}
            whileInView={{ opacity: 1 }}
            viewport={{ once: true }}
            className="section-title-wrap"
          >
            <h2 className="section-title">{t.latest}</h2>
            <div className="section-line"></div>
          </motion.div>

          {loading ? (
            <div className="bento-grid">
              {[1, 2, 3, 4].map(i => (
                <div key={i} className="bento-item skeleton glass-panel"></div>
              ))}
            </div>
          ) : (
            <>
              <div className="bento-grid">
                <AnimatePresence>
                  {posts.slice((currentPage - 1) * postsPerPage, currentPage * postsPerPage).map((post, index) => (
                  <motion.div
                    key={post.id}
                    initial={{ opacity: 0, scale: 0.9 }}
                    whileInView={{ opacity: 1, scale: 1 }}
                    viewport={{ once: true }}
                    transition={{ ...spring, delay: index * 0.1 }}
                    whileHover={{ scale: 1.02 }}
                    className={`bento-item glass-panel ${index === 0 ? 'bento-large' : ''}`}
                  >
                    <Link href={`/blog/${post.id}`} className="bento-link">
                      <div className="bento-image" style={{ backgroundImage: `url(${post.image})` }}>
                        <div className="bento-overlay"></div>
                      </div>
                      <div className="bento-content">
                        <div className="bento-meta">
                          <span className="bento-tag">{post.category}</span>
                          <span className="bento-meta-item"><Clock size={12} /> {post.date}</span>
                        </div>
                        <h3 className="bento-title">{post.title}</h3>
                        <p className="bento-excerpt">{post.summary}</p>
                        <div className="bento-footer">
                          <div className="bento-author">
                            <div className="avatar-mini"></div>
                            <span>{post.author}</span>
                          </div>
                          <div className="bento-arrow">
                            <ChevronRight size={18} />
                          </div>
                        </div>
                      </div>
                    </Link>
                  </motion.div>
                ))}
              </AnimatePresence>
              </div>

              {Math.ceil(posts.length / postsPerPage) > 1 && (
                <motion.div 
                  initial={{ opacity: 0, y: 20 }}
                  animate={{ opacity: 1, y: 0 }}
                  className="pagination-container glass-panel"
                >
                  <button 
                    onClick={() => setCurrentPage(p => Math.max(1, p - 1))}
                    disabled={currentPage === 1}
                    className="pagination-btn"
                  >
                    {t.prevBtn}
                  </button>
                  <span className="pagination-info">
                    <span className="text-accent">{currentPage}</span> / {Math.ceil(posts.length / postsPerPage)}
                  </span>
                  <button 
                    onClick={() => setCurrentPage(p => Math.min(Math.ceil(posts.length / postsPerPage), p + 1))}
                    disabled={currentPage === Math.ceil(posts.length / postsPerPage)}
                    className="pagination-btn"
                  >
                    {t.nextBtn}
                  </button>
                </motion.div>
              )}
            </>
          )}
        </div>
      </section>
    </div>
  );
}


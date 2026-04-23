'use client';
import { useEffect, useState } from 'react';
import Link from 'next/link';
import { motion, AnimatePresence } from 'framer-motion';
import { Edit3, Trash2, Plus, LayoutDashboard } from 'lucide-react';
import { apiService } from '@/services/api';
import '@/app/admin/admin.css';

const spring = {
  type: "spring",
  stiffness: 100,
  damping: 20
};

export default function AdminDashboard() {
  const [posts, setPosts] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetchPosts();
  }, []);

  const fetchPosts = async () => {
    setLoading(true);
    try {
      const results = await apiService.get('/posts');
      const dataArray = results?.data || results?.Data || [];
      
      const mapped = dataArray.map(item => ({
        id: item.blogId || item.id,
        title: item.blogTitle || item.title,
        author: item.blogAuthor || 'Admin',
        date: item.blogDate ? new Date(item.blogDate).toLocaleDateString() : 'Bugün',
        content: item.blogContent || item.content
      }));

      setPosts(mapped);
    } catch (err) {
      console.error(err);
      setPosts([]);
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async (id) => {
    if (confirm('Bu yazıyı silmek istediğinize emin misiniz?')) {
      await apiService.delete('/posts', id);
      fetchPosts();
    }
  };

  return (
    <div className="admin-wrapper">
      <motion.div 
        initial={{ opacity: 0, y: 20 }}
        animate={{ opacity: 1, y: 0 }}
        transition={spring}
        className="admin-container"
      >
        <header className="admin-header">
          <div className="admin-title-area">
            <h1 className="epic-title" style={{ fontSize: '3rem', margin: '0 0 0.5rem 0' }}>Otorite Paneli.</h1>
            <p className="admin-subtitle">Sistemin sinir ağı. İçerikleri buradan manipüle edebilirsiniz.</p>
          </div>
          <Link href="/admin/new" className="btn-premium flex-center">
            <Plus size={18} style={{ marginRight: '8px' }} /> Yeni İçerik Çekirdeği
          </Link>
        </header>

        <div className="admin-content glass-panel">
          {loading ? (
          <div className="loader"></div>
        ) : (
          <div className="admin-list">
            <div className="admin-list-header">
              <span>BAŞLIK</span>
              <span>YAZAR</span>
              <span>TARİH</span>
              <span className="text-right">İŞLEMLER</span>
            </div>
            <AnimatePresence>
              {posts.map((post, index) => (
                <motion.div 
                  key={post.id}
                  initial={{ opacity: 0, x: -20 }}
                  animate={{ opacity: 1, x: 0 }}
                  exit={{ opacity: 0, scale: 0.95 }}
                  transition={{ ...spring, delay: index * 0.05 }}
                  className="admin-list-item"
                >
                  <div className="col-title"><strong>{post.title}</strong></div>
                  <div className="col-author">{post.author}</div>
                  <div className="col-date">{post.date}</div>
                  <div className="col-actions">
                    <button className="action-pill edit-pill"><Edit3 size={16} /> Düzenle</button>
                    <button className="action-pill delete-pill" onClick={() => handleDelete(post.id)}><Trash2 size={16} /> Sil</button>
                  </div>
                </motion.div>
              ))}
            </AnimatePresence>
          </div>
        )}
        </div>
      </motion.div>
    </div>
  );
}

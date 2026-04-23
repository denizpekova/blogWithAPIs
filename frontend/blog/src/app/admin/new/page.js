'use client';
import { useState } from 'react';
import { useRouter } from 'next/navigation';
import { motion } from 'framer-motion';
import { ArrowLeft, Save } from 'lucide-react';
import { apiService } from '@/services/api';
import '@/app/admin/admin.css';

const spring = {
  type: "spring",
  stiffness: 100,
  damping: 20
};

export default function NewPost() {
  const router = useRouter();
  const [formData, setFormData] = useState({
    title: '',
    summary: '',
    image: '',
    content: ''
  });
  const [loading, setLoading] = useState(false);

  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);
    try {
      await apiService.post('/posts', formData);
      alert('Yazı başarıyla yayımlandı!');
      router.push('/admin');
    } catch (err) {
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="admin-wrapper">
      <motion.div 
        initial={{ opacity: 0, scale: 0.95 }}
        animate={{ opacity: 1, scale: 1 }}
        transition={spring}
        className="admin-container"
      >
        <header className="admin-header">
          <div className="admin-title-area">
            <h1 className="epic-title" style={{ fontSize: '3rem', margin: '0 0 0.5rem 0' }}>İçerik Çekirdeği İnşası.</h1>
            <p className="admin-subtitle">Sistemin merkezine yeni bir veri dokusu ekleyin.</p>
          </div>
          <button onClick={() => router.back()} className="btn-back">
            <ArrowLeft size={18} /> Otorite Paneline Dön
          </button>
        </header>

        <div className="glass-panel admin-form-card">
          <form onSubmit={handleSubmit} className="admin-form">
            <div className="form-group">
              <label>BAŞLIK</label>
              <input 
                type="text" 
                className="admin-input"
                placeholder="Örn: Nöral Ağların Geleceği..." 
                required 
                value={formData.title}
                onChange={(e) => setFormData({...formData, title: e.target.value})}
              />
            </div>

            <div className="form-group">
              <label>ÖZET</label>
              <textarea 
                className="admin-textarea"
                placeholder="Sisteme gönderilecek verinin kısa özeti..." 
                required
                rows="3"
                value={formData.summary}
                onChange={(e) => setFormData({...formData, summary: e.target.value})}
              ></textarea>
            </div>

            <div className="form-group">
              <label>KAPAK MATRİSİ (URL)</label>
              <input 
                type="url" 
                className="admin-input"
                placeholder="https://images.unsplash.com/..." 
                required
                value={formData.image}
                onChange={(e) => setFormData({...formData, image: e.target.value})}
              />
            </div>

            <div className="form-group">
              <label>KÜTÜPHANE İÇERİĞİ (MARKDOWN DESTEKLİ)</label>
              <textarea 
                className="admin-textarea"
                placeholder="Veri düğümlerini bu alana işleyin..." 
                required
                rows="10"
                value={formData.content}
                onChange={(e) => setFormData({...formData, content: e.target.value})}
              ></textarea>
            </div>

            <button type="submit" className="submit-btn flex-center" disabled={loading} style={{ justifyContent: 'center', gap: '8px' }}>
              {loading ? (
                'MATRİSE İŞLENİYOR...'
              ) : (
                <><Save size={20} /> AĞA ENJEKTE ET</>
              )}
            </button>
          </form>
        </div>
      </motion.div>
    </div>
  );
}

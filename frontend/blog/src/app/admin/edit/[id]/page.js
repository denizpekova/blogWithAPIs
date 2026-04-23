'use client';
import { useEffect, useState, use } from 'react';
import { useRouter } from 'next/navigation';
import { motion } from 'framer-motion';
import { ArrowLeft, Save, RefreshCw } from 'lucide-react';
import { apiService } from '@/services/api';
import '@/app/admin/admin.css';

const spring = {
  type: "spring",
  stiffness: 100,
  damping: 20
};

export default function EditPost({ params }) {
  const router = useRouter();
  const { id } = use(params);
  
  const [formData, setFormData] = useState({
    title: '',
    summary: '',
    image: '',
    content: '',
    createdDate: '',
    author: 'Admin'
  });
  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);

  useEffect(() => {
    const fetchPost = async () => {
      try {
        const result = await apiService.getById('/posts', id);
        const item = result?.data || result?.Data || result;
        
        if (item) {
          setFormData({
            title: item.blogTitle || item.title || item.Title || '',
            summary: item.blogSummary || item.summary || '',
            image: item.blogImage || item.imageUrl || item.ImageUrl || '',
            content: item.blogContent || item.content || item.Content || '',
            createdDate: (item.blogCreatedDate || item.createdDate || item.CreatedDate || '').split('T')[0],
            author: item.blogAuthor || item.author || item.Author || 'Admin'
          });
        }
      } catch (err) {
        console.error('Veri çekme hatası:', err);
        alert('Yazı verileri yüklenirken bir hata oluştu.');
      } finally {
        setLoading(false);
      }
    };
    fetchPost();
  }, [id]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    setSaving(true);
    try {
      const blogData = {
        Id: parseInt(id),
        Title: formData.title,
        Content: formData.content,
        ImageUrl: formData.image,
        Author: formData.author,
        CreatedDate: new Date(formData.createdDate).toISOString(),
        UpdatedDate: new Date().toISOString()
      };

      await apiService.put('/posts', blogData);
      alert('Yazı başarıyla güncellendi!');
      router.push('/admin');
    } catch (err) {
      console.error('Güncelleme hatası:', err);
      alert('Yazı güncellenirken bir hata oluştu.');
    } finally {
      setSaving(false);
    }
  };

  if (loading) {
    return (
      <div className="admin-wrapper flex-center">
        <div className="loader"></div>
      </div>
    );
  }

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
            <h1 className="epic-title" style={{ fontSize: '3rem', margin: '0 0 0.5rem 0' }}>Veri Düzenleme.</h1>
            <p className="admin-subtitle">Mevcut veri düğümünü yeniden konfigüre edin.</p>
          </div>
          <button onClick={() => router.back()} className="btn-back">
            <ArrowLeft size={18} /> Geri Dön
          </button>
        </header>

        <div className="glass-panel admin-form-card">
          <form onSubmit={handleSubmit} className="admin-form">
            <div className="form-group">
              <label>BAŞLIK</label>
              <input 
                type="text" 
                className="admin-input"
                required 
                value={formData.title}
                onChange={(e) => setFormData({...formData, title: e.target.value})}
              />
            </div>

            <div className="form-group">
              <label>KAPAK MATRİSİ (URL)</label>
              <input 
                type="url" 
                className="admin-input"
                required
                value={formData.image}
                onChange={(e) => setFormData({...formData, image: e.target.value})}
              />
            </div>

            <div className="form-row" style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '20px' }}>
              <div className="form-group">
                <label>YAZAR</label>
                <input 
                  type="text" 
                  className="admin-input"
                  required
                  value={formData.author}
                  onChange={(e) => setFormData({...formData, author: e.target.value})}
                />
              </div>

              <div className="form-group">
                <label>YAYIN TARİHİ</label>
                <input 
                  type="date" 
                  className="admin-input"
                  required
                  value={formData.createdDate}
                  onChange={(e) => setFormData({...formData, createdDate: e.target.value})}
                />
              </div>
            </div>

            <div className="form-group">
              <label>İÇERİK (MARKDOWN DESTEKLİ)</label>
              <textarea 
                className="admin-textarea"
                required
                rows="15"
                value={formData.content}
                onChange={(e) => setFormData({...formData, content: e.target.value})}
              ></textarea>
            </div>

            <button type="submit" className="submit-btn flex-center" disabled={saving} style={{ justifyContent: 'center', gap: '8px' }}>
              {saving ? (
                'GÜNCELLENİYOR...'
              ) : (
                <><RefreshCw size={20} /> DEĞİŞİKLİKLERİ KAYDET</>
              )}
            </button>
          </form>
        </div>
      </motion.div>
    </div>
  );
}

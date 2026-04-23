// C# API ile iletişim kuran servis katmanı
const API_BASE_URL = process.env.NEXT_PUBLIC_API_URL || 'http://localhost:5000/api';

// Geliştirme aşamasında API yoksa mock veri kullanmak için
const IS_MOCK = true;

const mockFetch = async (endpoint, options) => {
  console.log(`[Mock API Call] ${endpoint}`, options);
  await new Promise(r => setTimeout(r, 800)); // Simüle gecikme

  if (endpoint.includes('/auth/login')) {
    return { success: true, token: 'fake-jwt-token', user: { name: 'Ahmet Yılmaz', email: 'ahmet@example.com' } };
  }
  
  if (endpoint.includes('/posts')) {
    return Array.from({ length: 15 }, (_, i) => ({
      id: i + 1,
      title: `Test Fragment ${i + 1}: The Architect's Vision`,
      summary: `Bu, ${i + 1} numaralı otomatik test hücresidir. Sistemdeki sayfalama limitini (5 gönderi sınırı) ve geçiş dinamiklerini sınamak amacıyla yapay zeka tarafından geçici olarak simüle edilmiştir.`,
      author: 'The Architect',
      date: '2026-04-21',
      image: i % 3 === 0 
        ? 'https://images.unsplash.com/photo-1633356122544-f134324a6cee?q=80&w=2070' 
        : i % 3 === 1 
          ? 'https://images.unsplash.com/photo-1510915228340-29c85a43dcfe?q=80&w=2070' 
          : 'https://images.unsplash.com/photo-1586717791821-3f44a563dc4c?q=80&w=2070'
    }));
  }

  return { success: true };
};

export const apiService = {
  async get(endpoint) {
    if (IS_MOCK) return mockFetch(endpoint);
    const res = await fetch(`${API_BASE_URL}${endpoint}`);
    return res.json();
  },

  async getById(endpoint, id) {
    if (IS_MOCK) {
      const posts = await mockFetch('/posts');
      return posts.find(p => p.id == id) || posts[0];
    }
    const res = await fetch(`${API_BASE_URL}${endpoint}/${id}`);
    return res.json();
  },

  async post(endpoint, data) {
    if (IS_MOCK) return mockFetch(endpoint, { method: 'POST', body: data });
    const res = await fetch(`${API_BASE_URL}${endpoint}`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(data)
    });
    return res.json();
  },

  async put(endpoint, id, data) {
    if (IS_MOCK) return { success: true };
    const res = await fetch(`${API_BASE_URL}${endpoint}/${id}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(data)
    });
    return res.json();
  },

  async delete(endpoint, id) {
    if (IS_MOCK) return { success: true };
    const res = await fetch(`${API_BASE_URL}${endpoint}/${id}`, {
      method: 'DELETE'
    });
    return res.json();
  },

  async getCurrentUser() {
    if (IS_MOCK) {
      // Demo amaçlı: localStorage'da veri varsa onu döndür, yoksa null
      // Gerçekte C# API'dan (JWT ile) kullanıcı bilgisi ve rolü çekilecek
      const user = { name: 'Admin User', email: 'admin@example.com', role: 'Admin' };
      return user; 
    }
    const res = await fetch(`${API_BASE_URL}/auth/me`);
    return res.json();
  }
};

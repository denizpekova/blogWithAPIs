// Canlı ve Yerel ortam ayrımı
const IS_LOCAL = typeof window !== 'undefined' && window.location.hostname === 'localhost';
const API_BASE_URL = IS_LOCAL ? 'http://localhost:5279/api' : 'http://blog.mtapi.com.tr/api';
const IS_MOCK = false;

const getAuthHeader = () => {
  const token = typeof window !== 'undefined' ? localStorage.getItem('token') : null;
  return token ? { 'Authorization': `Bearer ${token}` } : {};
};

const mockFetch = async (endpoint, options) => {
  // ... (mock implementation remains for reference if needed)
  return { success: true };
};

export const apiService = {
  async get(endpoint) {
    if (IS_MOCK) return mockFetch(endpoint);
    const res = await fetch(`${API_BASE_URL}${endpoint}`, {
      headers: { ...getAuthHeader() }
    });
    return res.json();
  },

  async getById(endpoint, id) {
    if (IS_MOCK) {
      const posts = await mockFetch('/posts');
      return posts.find(p => p.id == id) || posts[0];
    }
    const res = await fetch(`${API_BASE_URL}${endpoint}/${id}`, {
      headers: { ...getAuthHeader() }
    });
    return res.json();
  },

  async post(endpoint, data) {
    if (IS_MOCK) return mockFetch(endpoint, { method: 'POST', body: data });
    const res = await fetch(`${API_BASE_URL}${endpoint}`, {
      method: 'POST',
      headers: { 
        'Content-Type': 'application/json',
        ...getAuthHeader()
      },
      body: JSON.stringify(data)
    });
    return res.json();
  },

  async put(endpoint, id, data) {
    if (IS_MOCK) return { success: true };
    const res = await fetch(`${API_BASE_URL}${endpoint}/${id}`, {
      method: 'PUT',
      headers: { 
        'Content-Type': 'application/json',
        ...getAuthHeader()
      },
      body: JSON.stringify(data)
    });
    return res.json();
  },

  async delete(endpoint, id) {
    if (IS_MOCK) return { success: true };
    const res = await fetch(`${API_BASE_URL}${endpoint}/${id}`, {
      method: 'DELETE',
      headers: { ...getAuthHeader() }
    });
    return res.json();
  },

  async getCurrentUser() {
    if (IS_MOCK) {
      return { name: 'Admin User', email: 'admin@example.com', role: 'Admin' };
    }
    const res = await fetch(`${API_BASE_URL}/auth/me`, {
      headers: { ...getAuthHeader() }
    });
    return res.json();
  }
};

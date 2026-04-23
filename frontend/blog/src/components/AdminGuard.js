'use client';
import { useEffect, useState } from 'react';
import { useRouter } from 'next/navigation';
import { apiService } from '@/services/api';

export default function AdminGuard({ children }) {
  const [authorized, setAuthorized] = useState(false);
  const [loading, setLoading] = useState(true);
  const router = useRouter();

  useEffect(() => {
    const checkAuth = async () => {
      try {
        const user = await apiService.getCurrentUser();
        if (user && user.role === 'Admin') {
          setAuthorized(true);
        } else {
          router.push('/auth/login?error=Unauthorized');
        }
      } catch (err) {
        router.push('/auth/login');
      } finally {
        setLoading(false);
      }
    };
    checkAuth();
  }, [router]);

  if (loading) return <div className="loader-container"><div className="loader"></div></div>;

  return authorized ? children : null;
}

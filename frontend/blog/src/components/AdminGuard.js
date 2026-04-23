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
        const response = await apiService.getCurrentUser();
        // Hem büyük harf hem küçük harf kontrolü (IsSuccess/isSuccess, Data/data, Role/role)
        const isSuccess = response?.isSuccess || response?.IsSuccess;
        const data = response?.data || response?.Data;
        const role = data?.role || data?.Role;

        if (isSuccess && role === 'Admin') {
          setAuthorized(true);
        } else {
          console.warn('Unauthorized access attempt:', response);
          router.push('/auth/login?error=Unauthorized');
        }
      } catch (err) {
        console.error('Guard error:', err);
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

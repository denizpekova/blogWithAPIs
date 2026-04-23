import { Inter } from "next/font/google";
import "./globals.css";
import Navbar from "@/components/Navbar";
import { LanguageProvider } from "@/contexts/LanguageContext";

const inter = Inter({
  subsets: ["latin"],
  variable: "--font-inter",
});

export const metadata = {
  title: "Modern Blog | Premium Deneyim",
  description: "Next.js ve C# API ile güçlendirilmiş modern blog platformu.",
};

export default function RootLayout({ children }) {
  return (
    <html lang="tr">
      <body className={`${inter.variable}`}>
        <LanguageProvider>
          <Navbar />
          <main>
            {children}
          </main>
        </LanguageProvider>
      </body>
    </html>
  );
}

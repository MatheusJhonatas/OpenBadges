// src/pages/HomePage.tsx
import { useNavigate } from "react-router-dom";
import { Button } from "../components/ui/Button";
import { FeatureCard } from "../components/ui/FeatureCard";
import { Award, ShieldCheck, Share2, QrCode } from "lucide-react";

export const HomePage = () => {
  const navigate = useNavigate();
  return (
    <div className="flex flex-col items-center text-center">
      {/* HERO */}
      <div className="w-full bg-white py-24 px-4 flex flex-col items-center">
        <Award size={64} className="text-blue-500 mb-6" />

        <h1 className="text-5xl font-bold mb-4">
          Sistema de Credenciais Digitais
        </h1>

        <p className="text-gray-700 mb-2">Núcleo de Formação - NTT DATA</p>

        <p className="text-gray-600 max-w-xl mb-6">
          Gerencie, compartilhe e verifique credenciais digitais profissionais
          seguindo o padrão Open Badges v2
        </p>

        <div className="flex gap-4">
          <Button onClick={() => navigate("/dashboard")}>
            Acessar Dashboard
          </Button>

          <Button variant="secondary" onClick={() => navigate("/meus-badges")}>
            Meus Badges
          </Button>
        </div>
      </div>

      {/* FUNCIONALIDADES */}
      <div className="w-full bg-gray-100 py-20 px-4">
        <h2 className="text-2xl font-bold mb-12 text-center">
          Funcionalidades Principais
        </h2>

        <div className="max-w-6xl mx-auto grid grid-cols-4 gap-6">
          <FeatureCard
            icon={<Award size={32} />}
            title="Gerenciar Badges"
            description="Visualize todas as suas credenciais digitais conquistadas em um único lugar"
          />

          <FeatureCard
            icon={<ShieldCheck size={32} />}
            title="Verificação"
            description="Sistema de verificação público seguindo o padrão Open Badges v2"
          />

          <FeatureCard
            icon={<Share2 size={32} />}
            title="Compartilhar"
            description="Compartilhe suas credenciais no LinkedIn, X/Twitter e WhatsApp"
          />

          <FeatureCard
            icon={<QrCode size={32} />}
            title="QR Code"
            description="Cada badge possui QR Code único para verificação rápida e segura"
          />
        </div>
      </div>
    </div>
  );
};

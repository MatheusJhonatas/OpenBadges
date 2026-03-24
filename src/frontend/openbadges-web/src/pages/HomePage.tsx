// src/pages/HomePage.tsx
import { useNavigate } from "react-router-dom";
import { Button } from "../components/ui/Button";

export const HomePage = () => {
  const navigate = useNavigate();
  return (
    <div className="flex flex-col items-center justify-center text-center mt-24 px-4 max-w-4xl mx-auto">
      
      <div className="text-blue-500 text-5xl mb-4">
        🎖️
      </div>

      <h1 className="text-5xl font-bold mb-4">
        Sistema de Credenciais Digitais
      </h1>

      <p className="text-black-600 mb-4">
        Núcleo de Formação - NTT DATA
      </p>

      <p className="text-black-800 max-w-xl mb-6">
        Gerencie, compartilhe e verifique credenciais digitais profissionais
        seguindo o padrão Open Badges v2
      </p>

      <div className="flex gap-4">
  <Button onClick={() => navigate("/dashboard")}>
    Acessar Dashboard
  </Button>

  <Button
    variant="secondary"
    onClick={() => navigate("/meus-badges")}
  >
    Meus Badges
  </Button>
</div>
      </div>
  );
};
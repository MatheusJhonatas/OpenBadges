// src/components/layout/Navbar.tsx
import { useLocation } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import { NavButton } from "../ui/NavButton";
import { Award } from "lucide-react";
import {
  Home,
  LayoutDashboard,
  User,
  Settings,
  BadgeCheck,
} from "lucide-react";

export const Navbar = () => {
  const location = useLocation();
  const navigate = useNavigate();

  return (
    <nav className="flex flex-wrap items-center justify-between px-4 md:px-8 py-4 bg-white border-b gap-4">
      <button
        onClick={() => navigate("/")}
        aria-label="Ir para página inicial"
        className="text-black-700 mb-4 cursor-pointer flex items-center gap-1 font-bold text-lg"
      >
        <Award className="text-blue-600 mr-2" /> <span className="text-sm md:text-base">Núcleo de Formação</span>
      </button>

      <div className="flex flex-wrap gap-2">
        <div className="flex gap-2">
          <NavButton
            icon={<Home size={16} />}
            active={location.pathname === "/"}
            onClick={() => navigate("/")}
          >
            Home
          </NavButton>

          <NavButton
            icon={<LayoutDashboard size={16} />}
            active={location.pathname === "/dashboard"}
            onClick={() => navigate("/dashboard")}
          >
            Dashboard
          </NavButton>

          <NavButton
            icon={<User size={16} />}
            active={location.pathname === "/meus-badges"}
            onClick={() => navigate("/meus-badges")}
          >
            Meus Badges
          </NavButton>

          <NavButton
            icon={<Settings size={16} />}
            active={location.pathname === "/admin/catalogo"}
            onClick={() => navigate("/admin/catalogo")}
          >
            Admin: Catálogo
          </NavButton>

          <NavButton
            icon={<BadgeCheck size={16} />}
            active={location.pathname === "/admin/emitir"}
            onClick={() => navigate("/admin/emitir")}
          >
            Admin: Emitir
          </NavButton>

          <NavButton
            icon={<BadgeCheck size={16} />}
            active={location.pathname === "/admin/revogar"}
            onClick={() => navigate("/admin/revogar")}
          >
            Admin: Revogar
          </NavButton>
        </div>
      </div>
    </nav>
  );
};

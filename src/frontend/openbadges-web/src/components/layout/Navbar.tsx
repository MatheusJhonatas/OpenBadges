// src/components/layout/Navbar.tsx
import { useLocation } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import { NavButton } from "../ui/NavButton";

export const Navbar = () => {
  const location = useLocation();
  const navigate = useNavigate();

  return (
    <nav className="flex items-center justify-between px-8 py-4 bg-white border-b">
      <button
        onClick={() => navigate("/")}
        aria-label="Ir para página inicial"
        className="text-gray-700 mb-4 cursor-pointer hover:bg-gray-200 px-2 py-1 rounded"
        >
   🏅 Núcleo de Formação - NTT DATA
    </button>

      <div className="flex gap-2">
            <div className="flex gap-2">
  <NavButton
    active={location.pathname === "/"}
    onClick={() => navigate("/")}
  >
    Home
  </NavButton>

  <NavButton
    active={location.pathname === "/dashboard"}
    onClick={() => navigate("/dashboard")}
  >
    Dashboard
  </NavButton>

  <NavButton
    active={location.pathname === "/meus-badges"}
    onClick={() => navigate("/meus-badges")}
  >
    Meus Badges
  </NavButton>

  <NavButton
    active={location.pathname === "/admin/catalogo"}
    onClick={() => navigate("/admin/catalogo")}
  >
    Admin: Catálogo
  </NavButton>

  <NavButton
    active={location.pathname === "/admin/emitir"}
    onClick={() => navigate("/admin/emitir")}
  >
    Admin: Emitir
  </NavButton>

  <NavButton
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
// src/components/layout/Navbar.tsx

import { NavButton } from "../ui/NavButton";

export const Navbar = () => {
  return (
    <nav className="flex items-center justify-between px-8 py-4 bg-white border-b">
      <button
        onClick={() => window.location.href = "/"}
        aria-label="Ir para página inicial"
        className="text-gray-700 mb-4 cursor-pointer hover:bg-gray-200 px-2 py-1 rounded"
        >
   🏅 Núcleo de Formação - NTT DATA
    </button>

      <div className="flex gap-2">
            <NavButton active>Home</NavButton>
            <NavButton>Dashboard</NavButton>
            <NavButton>Meus Badges</NavButton>
            <NavButton>Admin: Catálogo</NavButton>
            <NavButton>Admin: Emitir</NavButton>
            <NavButton>Admin: Revogar</NavButton>
    </div>
    </nav>
  );
};
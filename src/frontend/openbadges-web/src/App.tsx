import { Routes, Route } from "react-router-dom";
import { Navbar } from "./components/layout/Navbar";
import { HomePage } from "./pages/HomePage";
import { CatalogPage } from "./pages/CatalogPage";
import { BadgeDetailsPage } from "./pages/BadgeDetailsPage";
import { DashboardPage } from "./pages/DashboardPage";
import { IssuancePage } from "./pages/IssuancePage";
import { VerificationPage } from "./pages/VerificationPage";

function App() {
  return (
    <div className="min-h-screen bg-gray-100">
      <Navbar />

      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/dashboard" element={<DashboardPage />} />
        <Route path="/meus-badges" element={<div>Meus Badges</div>} />
        <Route path="/admin/catalogo" element={<CatalogPage />} />
        <Route path="/admin/catalogo/:id" element={<BadgeDetailsPage />} />
        <Route path="/verify/:code"  element={<VerificationPage />}/>
        <Route path="/admin/emitir" element={<IssuancePage />} />
        <Route path="/admin/revogar" element={<div>Admin: Revogar</div>} />
        
      </Routes>
    </div>
  );
}

export default App;
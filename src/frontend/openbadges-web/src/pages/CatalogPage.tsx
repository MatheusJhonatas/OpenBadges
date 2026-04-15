import { useEffect, useState, useRef } from "react";
import { BadgeCard } from "../components/ui/BadgeCard";
import { getBadges } from "../services/badgeService";

import type { Badge } from "../services/badgeService";
import { BadgeModal } from "../components/ui/BadgeModal";


export const CatalogPage = () => {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [badges, setBadges] = useState<Badge[]>([]);
  const [loading, setLoading] = useState(true);

  const openButtonRef = useRef<HTMLButtonElement>(null);

  const loadBadges = async () => {
    try {
      const data = await getBadges();
      setBadges(
        data.sort(
          (a, b) =>
            new Date(b.createdAt).getTime() -
            new Date(a.createdAt).getTime()
        )
      );
    } catch (error) {
      console.error("Erro ao buscar badges:", error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    loadBadges();
  }, []);

  return (
    <div className="p-8 max-w-6xl mx-auto">
      {/* HEADER */}
      <div className="flex justify-between items-center mb-6">
        <div>
          <h1 className="text-2xl font-bold">Catálogo de Badges</h1>
          <p className="text-gray-600">
            Gerencie os templates de credenciais disponíveis
          </p>
        </div>

        <button
          ref={openButtonRef}
          onClick={() => setIsModalOpen(true)}
          className="bg-blue-600 text-white px-4 py-2 rounded"
        >
          + Novo Badge
        </button>
      </div>

      {loading && <p>Carregando badges...</p>}
      {!loading && badges.length === 0 && <p>Nenhum badge encontrado</p>}

      {!loading && badges.length > 0 && (
        <div className="grid grid-cols-3 gap-6 items-stretch">
          {badges.map((badge) => (
            <BadgeCard key={badge.id} {...badge} />
          ))}
        </div>
      )}

      {/* MODAL */}
      <BadgeModal
        isOpen={isModalOpen}
        onClose={() => {
          setIsModalOpen(false);
          openButtonRef.current?.focus();
        }}
        onSuccess={loadBadges}
      />
    </div>
  );
};
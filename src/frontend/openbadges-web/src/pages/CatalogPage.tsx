import { useEffect, useState } from "react";
import { BadgeCard } from "../components/ui/BadgeCard";
import { getBadges } from "../services/badgeService";
import type { Badge } from "../services/badgeService";

export const CatalogPage = () => {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [badges, setBadges] = useState<Badge[]>([]);

  useEffect(() => {
    getBadges()
      .then(setBadges)
      .catch((error) => console.error("Erro ao buscar badges:", error));
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
          onClick={() => setIsModalOpen(true)}
          className="bg-blue-600 text-white px-4 py-2 rounded"
        >
          + Novo Badge
        </button>
      </div>

      {/* GRID */}
      <div className="grid grid-cols-3 gap-6 items-stretch">
        {badges.map((badge) => (
          <BadgeCard
            key={badge.id}
            name={badge.name}
            description={badge.description}
            imageUrl={badge.imageUrl}
          />
        ))}
      </div>
      {isModalOpen && (
        <div className="fixed inset-0 bg-black/50 flex items-center justify-center">
          <div className="bg-white p-6 rounded-lg w-96">
            <h2 className="text-lg font-bold mb-4">Novo Badge</h2>

            <p>Modal funcionando 👊</p>

            <button
              onClick={() => setIsModalOpen(false)}
              className="mt-4 bg-gray-200 px-4 py-2 rounded"
            >
              Fechar
            </button>
          </div>
        </div>
      )}
    </div>
  );
};

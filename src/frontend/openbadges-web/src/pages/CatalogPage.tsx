import { useEffect, useState } from "react";

type Badge = {
  id: string;
  name: string;
  description: string;
};

export const CatalogPage = () => {
  const [badges, setBadges] = useState<Badge[]>([]);

  useEffect(() => {
     fetch("http://localhost:5045/api/badges")
     .then((res) => res.json())
     .then((data) => {
       console.log("Badges recebidos:", data);
       setBadges(data);
     })
     .catch((error) => {
       console.error("Erro ao buscar badges:", error);
     });
  }, []);

  return (
    <div className="p-8 max-w-6xl mx-auto">

      <div className="flex justify-between items-center mb-6">
        <div>
          <h1 className="text-2xl font-bold">Catálogo de Badges</h1>
          <p className="text-gray-500">
            Gerencie os templates de credenciais disponíveis
          </p>
        </div>

        <button className="bg-blue-600 text-white px-4 py-2 rounded">
          + Novo Badge
        </button>
      </div>

      <div className="grid grid-cols-3 gap-6">
        {badges.map((badge) => (
          <div key={badge.id} className="border rounded p-4">
            <h3 className="font-bold">{badge.name}</h3>
            <p className="text-sm text-gray-500">{badge.description}</p>
          </div>
        ))}
      </div>

    </div>
  );
};
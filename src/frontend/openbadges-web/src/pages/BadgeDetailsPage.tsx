import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";

type BadgeDetails = {
  id: string;
  name: string;
  createdAt: string;
  description: string;
  slug: string;
  image?: {
    url: string;
  };
  criteria?: {
    narrative: string;
  };
};

export const BadgeDetailsPage = () => {
  const { id } = useParams();

  const [badge, setBadge] = useState<BadgeDetails | null>(null);

  useEffect(() => {
    if (!id) return;

    fetch(`http://localhost:5045/api/badges/${id}`)
      .then((res) => res.json())
      .then((data) => {
        console.log("Badge detalhe:", data);
        setBadge(data);
      })
      .catch((err) => console.error(err));
  }, [id]);
  const formatDate = (date: string) => {
    return new Date(date).toLocaleDateString("pt-BR", {
      day: "2-digit",
      month: "long",
      year: "numeric",
    });
  };
  return (
    <div className="p-8 flex justify-center">
      {badge ? (
        <div className="w-full max-w-2xl bg-white border rounded-xl shadow-sm p-6">
          {/* TÍTULO */}
          <div className="flex justify-between items-start mb-4">
            <div>
              <h2 className="text-xl font-semibold">{badge.name}</h2>
              <p className="text-sm text-gray-500">
                Emitido pelo Núcleo de Formação - NTT DATA
              </p>
            </div>

            {/* STATUS MOCK */}
            <span className="bg-blue-100 text-blue-700 text-xs px-3 py-1 rounded-full">
              Válido
            </span>
          </div>

          {/* BADGE (IMAGEM) */}
          <div className="flex justify-center my-6">
            {badge.image?.url && (
              <img
                src={`http://localhost:5173${badge.image.url}`}
                alt={badge.name}
                className="w-48"
              />
            )}
          </div>
          <div className="border-t border-gray-300 pt-4 mb-4 text-sm text-gray-500">
            Emitido em {formatDate(badge.createdAt)}
          </div>

          {/* DESCRIÇÃO */}
          <p className="text-gray-700 mb-4">{badge.description}</p>

          {/* CRITÉRIO */}
          {badge.criteria && (
            <p className="text-sm text-gray-600">
              <span className="font-semibold">Critérios:</span>{" "}
              {badge.criteria.narrative}
            </p>
          )}

          {/* QR CODE */}
          <div className="border-t pt-4 mt-6">
            <p className="text-sm font-medium mb-3">
              Código QR para Verificação
            </p>

            <div className="flex justify-center">
              <div className="w-32 h-32 bg-black text-white flex items-center justify-center rounded">
                QR Code
              </div>
            </div>
          </div>
        </div>
      ) : (
        <p>Carregando...</p>
      )}
    </div>
  );
};

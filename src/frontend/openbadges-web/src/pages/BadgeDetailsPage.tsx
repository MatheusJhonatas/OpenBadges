import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { ArrowLeft } from "lucide-react";

type BadgeDetails = {
  id: string;
  name: string;
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
  const navigate = useNavigate();

  const [badge, setBadge] = useState<BadgeDetails | null>(null);

  useEffect(() => {
    if (!id) return;

    fetch(`http://localhost:5045/api/badges/${id}`)
      .then((res) => res.json())
      .then(setBadge)
      .catch(console.error);
  }, [id]);

  const getImageUrl = (url?: string) => {
    if (!url) return "";
    return url.startsWith("http")
      ? url
      : `${window.location.origin}${url}`;
  };

  return (
    <div className="p-8 flex justify-center">
      <div className="w-full max-w-3xl">
        
        {/* VOLTAR */}
        <button
          onClick={() => navigate("/badges")}
          className="flex items-center gap-2 bg-blue-600 text-white px-4 py-2 rounded-lg text-sm hover:bg-blue-700 transition-colors mb-6"
        >
          <ArrowLeft size={16} />
          Voltar para Catálogo
        </button>

        {badge ? (
          <div className="bg-white border rounded-xl shadow-sm p-8">

            {/* HEADER */}
            <div className="mb-6">
              <h1 className="text-2xl font-bold">
                {badge.name}
              </h1>

              <p className="text-gray-500 mt-1">
                Emitido por Núcleo de Formação - NTT DATA
              </p>
            </div>

            {/* IMAGEM */}
            <div className="flex justify-center my-8">
              {badge.image?.url && (
                <img
                  src={getImageUrl(badge.image.url)}
                  alt={badge.name}
                  className="w-40"
                />
              )}
            </div>

            {/* DESCRIÇÃO */}
            <div className="mb-6">
              <h2 className="font-semibold mb-2">
                Descrição
              </h2>

              <p className="text-gray-700">
                {badge.description}
              </p>
            </div>

            {/* CRITÉRIOS */}
            {badge.criteria && (
              <div className="mb-6">
                <h2 className="font-semibold mb-2">
                  Critérios
                </h2>

                <p className="text-gray-700">
                  {badge.criteria.narrative}
                </p>
              </div>
            )}

            {/* COMO CONQUISTAR */}
            <div className="mb-6">
              <h2 className="font-semibold mb-2">
                Como conquistar
              </h2>

              <ul className="list-disc ml-6 text-gray-700 space-y-1">
                <li>Completar trilha técnica relacionada</li>
                <li>Entregar projeto prático</li>
                <li>Passar na mentoria técnica</li>
              </ul>
            </div>

          </div>
        ) : (
          <p className="text-gray-500">
            Carregando badge...
          </p>
        )}
      </div>
    </div>
  );
};
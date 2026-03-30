import { useNavigate } from "react-router-dom";

type BadgeCardProps = {
  id: string;
  name: string;
  slug?: string;
  description: string;
  criteria?: string;
  imageUrl?: string;
};

export const BadgeCard = ({
  id,
  name,
  slug,
  description,
  criteria,
  imageUrl,
}: BadgeCardProps) => {
  const navigate = useNavigate();

  return (
    <div className="rounded-xl overflow-hidden border bg-white shadow-sm hover:shadow-md transition flex flex-col h-full">
      {/* PREVIEW */}
      <div className="h-40 bg-gray-100 flex items-center justify-center">
        {imageUrl ? (
          <img src={imageUrl} alt={name} className="h-24 object-contain" />
        ) : (
          <span className="text-black-600 text-sm">Sem imagem</span>
        )}
      </div>

      {/* CONTEÚDO */}
      <div className="p-4 flex flex-col flex-1 justify-between">
        <div>
          <h3 className="font-semibold text-base mb-1">{name}</h3>

          {slug && (
            <span className="inline-block bg-gray-100 text-gray-700 text-xs font-semibold px-3 py-1 rounded-full mb-3">
              {slug}
            </span>
          )}
          <p className="text-sm text-gray-700 mb-3">{description}</p>

          {criteria && (
            <p className="text-xs text-gray-600 mt-2">
              <span className="font-semibold text-gray-800">Critérios:</span>{" "}
              <span className="text-gray-600">{criteria}</span>
            </p>
          )}
        </div>
        {/* AÇÕES */}
        <div className="flex gap-2 mt-4">
          <button className="flex-1 border rounded px-3 py-1 text-sm hover:bg-gray-100">
            Editar
          </button>

          <button
          onClick={() => navigate(`/admin/catalogo/${id}`)}
          className="flex-1 border rounded px-3 py-1 text-sm hover:bg-gray-100"
            >
            Ver Detalhes
          </button>
        </div>
      </div>
    </div>
  );
};

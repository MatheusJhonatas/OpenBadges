type BadgeCardProps = {
  name: string;
  description: string;
  imageUrl?: string;
};

export const BadgeCard = ({ name, description, imageUrl }: BadgeCardProps) => {
  return (
    <div className="rounded-xl overflow-hidden border bg-white shadow-sm hover:shadow-md transition flex flex-col h-full">
      {/* PREVIEW */}
      <div className="h-40 bg-gray-100 flex items-center justify-center">
        {imageUrl ? (
          <img src={imageUrl} alt={name} className="h-24 object-contain" />
        ) : (
          <span className="text-gray-400 text-sm">Sem imagem</span>
        )}
      </div>

      {/* CONTEÚDO */}
      <div className="p-4 flex flex-col flex-1 justify-between">
        <div>
          <h3 className="font-semibold mb-2">{name}</h3>
          <p className="text-sm text-gray-800">{description}</p>
        </div>

        {/* AÇÕES (sempre embaixo) */}
        <div className="flex gap-2 mt-4">
          <button className="flex-1 border rounded px-3 py-1 text-sm hover:bg-black-400">
            Editar
          </button>

          <button className="flex-1 border rounded px-3 py-1 text-sm hover:bg-black-400">
            Ver Detalhes
          </button>
        </div>
      </div>
    </div>
  );
};

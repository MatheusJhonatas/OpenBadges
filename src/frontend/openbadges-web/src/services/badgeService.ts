type BadgeApiResponse = {
  id: string;
  name: string;
  slug?: string;
  description: string;
  createdAt: string;
  version: number;
templateId: {
  value: string;
};
  criteria?: {
    narrative: string;
  };
};

export type Badge = {
  id: string;
  name: string;
  slug: string;
  description: string;
  criteria: string;
  templateId: { value: string };
  createdAt: string;
  version: number;
  imageUrl?: string;//temporario, para exibir a imagem gerada no catálogo. O ideal seria o backend já retornar isso, mas por ora vamos gerar na hora.
};

export const getBadges = async (): Promise<Badge[]> => {
  const response = await fetch("http://localhost:5045/api/badges");

  // 🔥 Tratamento de erro HTTP (importante)
  if (!response.ok) {
    throw new Error("Erro ao buscar badges");
  }

  const data = await response.json();

  // 🔹 Compatível com array direto ou { data: [...] }
  const rawBadges: BadgeApiResponse[] = Array.isArray(data)
    ? data
    : data.data;

  // 🔹 Mapping backend → frontend
  return rawBadges.map((b) => ({
    id: b.id,
    name: b.name,
    slug: b.slug ?? "sem-slug",
    description: b.description,
    criteria: b.criteria?.narrative ?? "Sem critérios definidos",
    templateId: b.templateId ? { value: b.templateId.value } : { value: "" },
    createdAt: b.createdAt ?? "",
    version: b.version,
  }));
};
type BadgeApiResponse = {
  id: string;
  name: string;
  slug?: string;
  description: string;
  createdAt: string;
  image?: {
    url: string;
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
  imageUrl?: string;
  createdAt: string;
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
    imageUrl: b.image?.url,
    createdAt: b.createdAt ?? "",
  }));
};
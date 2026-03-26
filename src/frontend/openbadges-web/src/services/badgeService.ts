type BadgeApiResponse = {
  id: string;
  name: string;
  description: string;
  image?: {
    url: string;
  };
};

export type Badge = {
  id: string;
  name: string;
  description: string;
  imageUrl?: string;
};

export const getBadges = async (): Promise<Badge[]> => {
  const response = await fetch("http://localhost:5045/api/badges");

  const data = await response.json();

  const rawBadges = Array.isArray(data) ? data : data.data;

  return rawBadges.map((b: BadgeApiResponse) => ({
    id: b.id,
    name: b.name,
    description: b.description,
    imageUrl: b.image?.url,
  }));
};
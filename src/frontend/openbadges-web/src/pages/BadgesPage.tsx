import { useEffect, useState } from "react";
import { getBadges } from "../api/badgeApi";

type Badge = {
  id: string;
  name: string;
  description: string;
  slug: string;
};

function BadgesPage() {
  const [badges, setBadges] = useState<Badge[]>([]);
  const [error, setError] = useState<string | null>(null);
  const loadBadges = () => {
  getBadges()
    .then(setBadges)
    .catch(() => setError("Falha ao carregar badges"));
};

  useEffect(() => {
  loadBadges();
}, []);

  if (error) return <div>{error}</div>;

  return (
    <div>
      <h1>Badges</h1>
      <button onClick={loadBadges}>Atualizar</button>
      <ul>
        {badges.map(b => (
          <li key={b.id}>
            <strong>{b.name}</strong> — {b.description} ({b.slug})
          </li>
        ))}
      </ul>
    </div>
  );
}

export default BadgesPage;
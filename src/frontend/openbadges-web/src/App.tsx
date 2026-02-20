import { useEffect, useState } from "react";
import { getBadges } from "./api/badgeApi";

type Badge = {
  id: string;
  name: string;
  description: string;
  slug: string;
};

function App() {
  const [badges, setBadges] = useState<Badge[]>([]);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    getBadges()
      .then(setBadges)
      .catch(() => setError("Falha ao carregar badges"));
  }, []);

  if (error) return <div>{error}</div>;

  return (
    <div>
      <h1>Badges</h1>
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

export default App;
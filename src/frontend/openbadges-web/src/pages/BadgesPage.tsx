import { useEffect, useState } from "react";
import { getBadges, createBadge } from "../api/badgeApi";
import BadgeCard from "../components/BadgeCard";

type Badge = {
  id: string;
  name: string;
  description: string;
  slug: string;
};

function BadgesPage() {
  const [badges, setBadges] = useState<Badge[]>([]);
  const [error, setError] = useState<string | null>(null);

  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [imageUrl, setImageUrl] = useState("");
  const [criteriaNarrative, setCriteriaNarrative] = useState("");

  const loadBadges = () => {
    getBadges()
      .then(setBadges)
      .catch(() => setError("Falha ao carregar badges"));
  };

  useEffect(() => {
    loadBadges();
  }, []);

  const handleCreate = async () => {
    try {
      await createBadge({
        name,
        description,
        imageUrl,
        criteriaNarrative
      });

      setName("");
      setDescription("");
      setImageUrl("");
      setCriteriaNarrative("");

      loadBadges();
    } catch {
      setError("Falha ao criar badge");
    }
  };

  if (error) return <div>{error}</div>;

  return (
    <div>
      <h1>Badges</h1>

      <div style={{ marginBottom: "20px" }}>
        <h2>Criar Badge</h2>

        <input
          placeholder="Nome"
          value={name}
          onChange={e => setName(e.target.value)}
        />
        <br />

        <input
          placeholder="Descrição"
          value={description}
          onChange={e => setDescription(e.target.value)}
        />
        <br />

        <input
          placeholder="Image URL"
          value={imageUrl}
          onChange={e => setImageUrl(e.target.value)}
        />
        <br />

        <input
          placeholder="Criteria"
          value={criteriaNarrative}
          onChange={e => setCriteriaNarrative(e.target.value)}
        />
        <br />

        <button onClick={handleCreate}>Criar</button>
      </div>

      <div>
        {badges.map(b => (
          <BadgeCard key={b.id} badge={b} />
        ))}
      </div>
    </div>
  );
}

export default BadgesPage;
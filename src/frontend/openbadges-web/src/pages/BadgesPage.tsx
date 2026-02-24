import { useEffect, useState } from "react";
import { getBadges, createBadge } from "../api/badgeApi";
import BadgeCard from "../components/BadgeCard";

import type { Badge } from "../types/Badge";

function BadgesPage() {
  const [badges, setBadges] = useState<Badge[]>([]);
  const [error, setError] = useState<string | null>(null);
  const [loading, setLoading] = useState(false);
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [imageUrl, setImageUrl] = useState("");
  const [criteriaNarrative, setCriteriaNarrative] = useState("");

  const loadBadges = () => {
    setLoading(true);
    getBadges()
      .then(data => {
        setBadges(data);
        setError(null);
      })
      .catch(() => setError("Falha ao carregar badges"))
      .finally(() => setLoading(false));
  };

  useEffect(() => {
    loadBadges();
  }, []);

  const handleCreate = async () => {
    try {
      setLoading(true);
      const created = await createBadge({
        name,
        description,
        imageUrl,
        criteriaNarrative
      });
      setBadges(prev => [...prev, created]);

      setError(null);

      setName("");
      setDescription("");
      setImageUrl("");
      setCriteriaNarrative("");

    } catch {
      setError("Falha ao criar badge");
    } finally {
      setLoading(false);
    }
  };



  return (
    <div>
      <h1>Badges</h1>
      {loading && <div>Carregando...</div>}
      {error && (
  <div style={{ color: "red", marginBottom: "10px" }}>
    {error}
  </div>
)}
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
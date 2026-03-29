import type { Badge } from "../types/Badge";

type Props = {
  badge: Badge;
};

function BadgeCard({ badge }: Props) {
  return (
    <div style={{
      border: "1px solid #ccc",
      padding: "12px",
      marginBottom: "8px",
      borderRadius: "6px"
    }}>
      <h3>{badge.name}</h3>
      <p>{badge.slug}</p>
      <p>{badge.description}</p>
      <p>{badge.criteria}</p>
    </div>
  );
}

export default BadgeCard;
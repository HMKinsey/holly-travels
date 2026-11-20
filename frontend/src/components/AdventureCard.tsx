import type { Adventure } from "../types/adventure";

interface AdventureCardProps {
  adventure: Adventure;
}

function AdventureCard({ adventure }: AdventureCardProps) {
  return (
    <article
      style={{
        border: "1px solid #cbd5e1",
        borderRadius: 8,
        padding: 16,
        marginBottom: 16,
      }}
    >
      {adventure.photoUrl && (
        <img
          src={adventure.photoUrl}
          alt={`${adventure.title} in ${adventure.country}`}
          style={{
            width: "100%",
            maxHeight: 240,
            objectFit: "cover",
            borderRadius: 6,
          }}
        />
      )}
      <p
        style={{
          fontSize: 13,
          color: "#0f766e",
          fontWeight: 700,
          margin: "10px 0 4px",
        }}
      >
        {adventure.country} • {adventure.visitDate}
      </p>
      <h2 style={{ margin: "0 0 8px", fontSize: 18 }}>{adventure.title}</h2>
    </article>
  );
}

export default AdventureCard;

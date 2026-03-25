import type React from "react";

type FeatureCardProps = {
  icon: React.ReactNode;
  title: string;
  description: string;
};

export const FeatureCard = ({
  icon,
  title,
  description,
}: FeatureCardProps) => {
  return (
    <div className="border rounded-xl p-6 bg-white shadow-sm hover:shadow-md transition text-left">
      
      <div className="text-blue-600 text-3xl mb-4">
        {icon}
      </div>

      <h3 className="font-semibold text-lg mb-2">
        {title}
      </h3>

      <p className="text-sm text-gray-500">
        {description}
      </p>

    </div>
  );
};
-- CreateTable
CREATE TABLE "Event" (
    "createdAt" TIMESTAMP(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "deviceId" TEXT,
    "eventData" JSONB,
    "eventType" TEXT,
    "id" TEXT NOT NULL,
    "timestamp" TIMESTAMP(3),
    "updatedAt" TIMESTAMP(3) NOT NULL,

    CONSTRAINT "Event_pkey" PRIMARY KEY ("id")
);

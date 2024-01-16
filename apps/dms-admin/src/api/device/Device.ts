import { Event } from "../event/Event";

export type Device = {
  createdAt: Date;
  deviceId: string | null;
  deviceName: string | null;
  events?: Array<Event>;
  id: string;
  updatedAt: Date;
};

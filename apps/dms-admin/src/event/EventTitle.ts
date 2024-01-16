import { Event as TEvent } from "../api/event/Event";

export const EVENT_TITLE_FIELD = "eventType";

export const EventTitle = (record: TEvent): string => {
  return record.eventType?.toString() || String(record.id);
};

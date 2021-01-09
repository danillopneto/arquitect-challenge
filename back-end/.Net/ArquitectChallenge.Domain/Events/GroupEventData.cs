namespace ArquitectChallenge.Domain.Events
{
    public class GroupEventData
    {
        public int Quantity { get; set; }

        public string Tag { get; set; }

        public string FirstTag()
        {
            if (string.IsNullOrWhiteSpace(Tag)
                    || !Tag.Contains("."))
            {
                return null;
            }

            return Tag.Split(".")[0];
        }

        public string SecondTag()
        {
            if (string.IsNullOrWhiteSpace(Tag)
                    || !Tag.Contains("."))
            {
                return null;
            }

            var tags = Tag.Split(".");
            if (tags.Length == 1)
            {
                return null;
            }

            return string.Join(".", tags[0], tags[1]);
        }
    }
}
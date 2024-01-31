using NotesWithAutotagging.Infrastructure.Notes;

namespace NotesWithAutotagging.Tests.Infrastructure.Notes
{
    [TestFixture]
    public class AutoTaggerTests
    {
        [TestCase("a@a.a")]
        [TestCase("mail a@a.a")]
        public void AutoTagger_TagNote_ReturnsEmail(string note)
        {
            var autoTagger = new AutoTagger(note);
            var result = autoTagger.TagNote();

            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First(), Is.EqualTo("EMAIL"));
        }

        [TestCase(" 1")]
        [TestCase("1")]
        [TestCase("555-5555-555")]
        [TestCase("+48 504 203 260")]
        [TestCase("+48 (12) 504 203 260")]
        [TestCase("+48 (12) 504-203-260")]
        [TestCase("+48(12)504203260")]
        [TestCase("+4812504203260")]
        [TestCase("4812504203260")]
        [TestCase("+48.504.203.260")]
        [TestCase("504.203.260")]
        public void AutoTagger_TagNote_ReturnsPhone(string note)
        {
            var autoTagger = new AutoTagger(note);
            var result = autoTagger.TagNote();

            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First(), Is.EqualTo("PHONE"));
        }

        [TestCase("a@a")]
        [TestCase("Lorem ipsum dolor sit amet")]
        public void AutoTagger_TagNote_NotReturnsEmail(string note)
        {
            var autoTagger = new AutoTagger(note);
            var result = autoTagger.TagNote();

            Assert.That(result.Count(), Is.EqualTo(0));
        }

        [TestCase("")]
        [TestCase("+48 504 203 260@@")]
        [TestCase("+55(123) 456-78-90-")]
        [TestCase(" ")]
        [TestCase("-")]
        [TestCase("()")]
        [TestCase("() + ()")]
        [TestCase("+48 (21)")]
        [TestCase("+")]
        [TestCase("Lorem ipsum dolor sit amet")]
        public void AutoTagger_TagNote_NotReturnsPhone(string note)
        {
            var autoTagger = new AutoTagger(note);
            var result = autoTagger.TagNote();

            Assert.That(result.Count(), Is.EqualTo(0));
        }
    }
}

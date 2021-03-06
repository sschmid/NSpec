using FluentAssertions;
using NUnit.Framework;

namespace NSpec.Tests.WhenRunningSpecs.BeforeAndAfter
{
    [TestFixture]
    public class abstract_class : when_running_specs
    {
        abstract class Abstract : sequence_spec
        {
            void before_all()
            {
                sequence = "A";
            }

            void before_each()
            {
                sequence += "C";
            }

            void a_context()
            {
                beforeAll = () => sequence += "B";

                before = () => sequence += "D";
                specify = () => Assert.That(true, Is.True);
                after = () => sequence += "E";

                afterAll = () => sequence += "G";
            }

            void after_each()
            {
                sequence += "F";
            }

            void after_all()
            {
                sequence += "H";
            }
        }

        class Concrete : Abstract {}

        [Test]
        public void all_features_are_supported_from_abstract_classes_when_run_under_the_context_of_a_derived_concrete()
        {
            Run(typeof(Concrete));
            Concrete.sequence.Should().Be("ABCDEFGH");
        }
    }
}
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace NSpec.Tests.WhenRunningSpecs.BeforeAndAfter
{
    [TestFixture]
    [Category("RunningSpecs")]
    [Category("Async")]
    public class async_class_levels_and_context_methods : when_running_specs
    {
        class SpecClass : sequence_spec
        {
            async Task before_all()
            {
                await Task.Run(() => sequence = "A");
            }

            async Task before_each()
            {
                await Task.Run(() => sequence += "C");
            }

            void a_context()
            {
                beforeAllAsync = async () => await Task.Run(() => sequence += "B");

                beforeAsync = async () => await Task.Run(() => sequence += "D");
                specify = () => Assert.That(true, Is.True);
                afterAsync = async () => await Task.Run(() => sequence += "E");

                afterAllAsync = async () => await Task.Run(() => sequence += "G");
            }

            async Task after_each()
            {
                await Task.Run(() => sequence += "F");
            }

            async Task after_all()
            {
                await Task.Run(() => sequence += "H");
            }
        }

        [SetUp]
        public void setup()
        {
            Run(typeof(SpecClass));
        }

        [Test]
        public void before_alls_at_every_level_run_before_before_eaches_from_the_outside_in()
        {
            SpecClass.sequence.Should().StartWith("ABCD");
        }

        [Test]
        public void after_alls_at_every_level_run_after_after_eaches_from_the_inside_out()
        {
            SpecClass.sequence.Should().EndWith("EFGH");
        }
    }
}

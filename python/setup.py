import setuptools

with open("README.md", "r") as fh:
    long_description = fh.read()

setuptools.setup(
    name="linksgql",
    version="0.1.0",
    author="Ethosa",
    author_email="social.ethosa@gmail.com",
    description="Data.Doublets.Gql",
    long_description=long_description,
    long_description_content_type="text/markdown",
    url="https://github.com/linksplatform/Data.Doublets.Gql",
    packages=setuptools.find_packages(),
    license="LGPLv3",
    keywords="Python Gql",
    classifiers=[
        "Development Status :: 4 - Beta",
        "Programming Language :: Python :: 3",
        "Programming Language :: Python :: 3.4",
        "Programming Language :: Python :: 3.5",
        "Programming Language :: Python :: 3.6",
        "Programming Language :: Python :: 3.7",
        "License :: OSI Approved :: GNU Affero General Public License v3.0 (AGPLv3)",
        "Operating System :: OS Independent",
    ],
    project_urls={
        "Github": "https://github.com/linksplatform/Data.Doublets.Gql",
        "Documentation": "https://github.com/linksplatform/Data.Doublets.Gql",
    },
    python_requires=">=3",
    install_requires=[
        "graphene",  # GraphQL
    ]
)
